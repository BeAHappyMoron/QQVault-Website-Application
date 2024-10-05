using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QQVault.Data;
using QQVault.Models;
using QQVault.Models.ViewModels;

namespace QQVault.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepositoryWrapper _repo;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IRepositoryWrapper repo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _repo = repo;
        }

        [TempData]
        public string Message { get; set; }

        // Index action restricted to administrators, consultants, and financial advisers
        public IActionResult Index()
        {
            var users = _repo.User.FindAll();
            return View(users);
        }

        // Profile action restricted to clients only
        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Profile()
        {
            // Get the currently logged-in user using UserManager
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Use FindByCondition for fetching bank accounts related to the user
            var bankAccounts = _repo.BankAccount.FindByCondition(b => b.UserId == user.Id).ToList();

            var viewModel = new UserDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TotalBalance = bankAccounts.Sum(b => b.Balance),
                UserStatus = bankAccounts.Sum(b => b.Balance) > 0 ? "Active" : "Inactive",
                BankAccounts = bankAccounts.Select(b => new BankAccountViewModel
                {
                    AccountNumber = b.AccountNumber,
                    Balance = b.Balance
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new UserCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.IsValidNumberInput())
                {
                    ModelState.AddModelError("NumberInput", $"For {model.UserType}, the number must be of valid length.");
                }
                else
                {
                    var user = new User
                    {
                        UserName = model.Username,
                        UserType = model.UserType,
                        NumberInput = model.NumberInput,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        CellPhoneNumber = model.CellPhoneNumber,
                        CreatedAt = DateTime.Now,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        // Add role using enum value
                        await _userManager.AddToRoleAsync(user, model.SelectedRole.ToString());
                        Message = $"{user.LastName} {user.FirstName} added successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Handle user not found
            }

            else
            {
                return View(user);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string NumberInput, string FirstName, string LastName, string Email, string CellPhoneNumber, UserType UserType, string Password)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            if (!ModelState.IsValid)
            {
                return View(user); // Return the view if model state is invalid
            }

            user.UserType = UserType;
            user.NumberInput = NumberInput;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.CellPhoneNumber = CellPhoneNumber;

            if (!string.IsNullOrEmpty(Password))
            {
                if (await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.RemovePasswordAsync(user);
                }

                var validPass = await _userManager.AddPasswordAsync(user, Password);
                if (!validPass.Succeeded)
                {
                    foreach (var error in validPass.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(user); // Return view if adding password fails
                }
            }

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Message = $"{user.LastName} {user.FirstName} modified successfully.";
                return RedirectToAction("Index"); // Redirect on success
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user); // Return the view if update fails
        }


        public IActionResult Delete(string id)
        {
            var user = _repo.User.GetById(id);
            if (user == null)
                return NotFound();
            else
            {
                return View(user);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteUser(string id)
        {
            var user = _repo.User.GetById(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Unable to delete user as it no longer exists.";
                return View();
            }

            try
            {
                _repo.User.Delete(user);
                _repo.Save();
                Message = $"{user.LastName} {user.FirstName} deleted successfully.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.ErrorMessage = "The user was deleted by another process. Please refresh the list.";
                return View(user);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Unable to delete user: {ex.Message}";
                return View(user);
            }
        }

        public IActionResult Details(string id)
        {
            // Fetch the user based on the provided ID
            var user = _repo.User.GetById(id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if user is not found
            }

            // Fetch the user's bank accounts
            var bankAccounts = _repo.BankAccount
                                  .FindByCondition(account => account.UserId == user.Id)
                                  .Select(account => new BankAccountViewModel
                                  {
                                      AccountNumber = account.AccountNumber,
                                      Balance = account.Balance
                                  }).ToList();

            // Calculate the total balance of the user's bank accounts
            decimal totalBalance = bankAccounts.Sum(account => account.Balance);

            var advices = user.Advices ?? new List<Advice>();

            // Create the UserDetailsViewModel
            var userDetailsViewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TotalBalance = totalBalance,
                UserStatus = GetUserStatus(totalBalance), // Assuming this method exists
                BankAccounts = bankAccounts, // This was missing a comma
                Advices = user.Advices.Select(a => new AdviceViewModel
                {
                    ConsultantName = a.ConsultantName,
                    Message = a.Message,
                    CreatedAt = a.CreatedAt
                }).ToList()
            };

            // Return the view with the user details
            return View(userDetailsViewModel); // Ensure you're returning the correct model
        }


        private string GetUserStatus(decimal totalBalance)
        {
            if (totalBalance <= 500)
                return "Critical"; // Critical status
            else if (totalBalance > 500 && totalBalance < 3000)
                return "Moderate"; // Moderate status
            else
                return "Healthy"; // Healthy status
        }

    }
}
