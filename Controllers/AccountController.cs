using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QQVault.Models;
using static QQVault.Models.ViewModels.UserViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QQVault.Data;

namespace QQVault.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repo;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IRepositoryWrapper repo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            // Find user by NumberInput instead of Email
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NumberInput == loginModel.NumberInput);

            if (user != null)
            {
                // Check if the password is correct
                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

     

                if (result.Succeeded)
                {
                    // Check for roles and redirect accordingly
                    if (await _userManager.IsInRoleAsync(user, "Administrator") ||
                        await _userManager.IsInRoleAsync(user, "Consultant") ||
                        await _userManager.IsInRoleAsync(user, "FinancialAdviser"))
                    {
                        // Redirect to the AdminDashboard if the user has an admin-type role
                        return Redirect(loginModel?.ReturnUrl ?? "/Dashboard/AdminDashboard");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Client"))
                    {
                        // Redirect to the ClientDashboard if the user is a client
                        return Redirect(loginModel?.ReturnUrl ?? "/Dashboard/ClientDashboard");
                    }

                    // Handle other roles or default
                    return Redirect(loginModel?.ReturnUrl ?? "/Dashboard/ClientDashboard");
                }
            }

            // If the login failed, return the login view with the provided model
            return View(loginModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    CellPhoneNumber = registerModel.CellPhoneNumber,
                    NumberInput = registerModel.NumberInput,
                    CreatedAt = DateTime.Now,
                    UserType = registerModel.UserType
                };

                user.UserName = registerModel.FirstName;


                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    // Assign the "Client" role to the new user
                    await _userManager.AddToRoleAsync(user, "Client");

                    string accountNumberPrefix = user.UserType switch
                    {
                        UserType.Student => "STU",
                        UserType.Staff => "UFS",
                        UserType.Guest => "GST",
                        _ => "UNK"
                    };

                    string randomSuffix = new Random().Next(1000000, 9999999).ToString();
                    string accountNumber = accountNumberPrefix + randomSuffix;

                    var bankAccount = new BankAccount
                    {
                        AccountNumber = accountNumber,
                        UserId = user.Id,
                        Balance = 500
                    };

                    _repo.BankAccount.Create(bankAccount);
                    _repo.Save();

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors.Select(x => x.Description))
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
