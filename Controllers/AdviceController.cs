using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QQVault.Data;
using QQVault.Models;
using QQVault.Models.ViewModels;
using System;

[Authorize(Roles = "FinancialAdviser")]
public class AdviceController : Controller
{
    private readonly IRepositoryWrapper _repo;

    public AdviceController(IRepositoryWrapper repo)
    {
        _repo = repo;
    }

    // GET: Advice/Create
    public IActionResult Create(string userId)
    {
        var adviceViewModel = new AdviceViewModel
        {
            UserId = userId
        };
        return View(adviceViewModel);
    }

    // POST: Advice/Create
    [HttpPost]
    public IActionResult Create(AdviceViewModel model)
    {
        if (ModelState.IsValid)
        {
            var advice = new Advice
            {
                UserId = model.UserId,
                Message = model.Message,
                CreatedAt = DateTime.Now,
                ConsultantName = User.Identity.Name // Assuming you're using authentication
            };

            _repo.Advice.Create(advice);
            _repo.Save();

            return RedirectToAction("Details", "Users", new { id = model.UserId });
        }

        return View(model);
    }

    public IActionResult LowBalanceAccounts()
    {
        // Use FindByCondition to fetch accounts with balance less than 500
        var accounts = _repo.BankAccount.FindByCondition(account => account.Balance < 500).ToList();

        return View(accounts); // Pass the filtered accounts to the view
    }

}
