using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QQVault.Data;
using QQVault.Models;
using QQVault.Models.ViewModels;
using System;


public class TransactionsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryWrapper _repo;

    public TransactionsController(IRepositoryWrapper repo, UserManager<User> userManager)
    {
        _userManager = userManager;
        _repo = repo;
    }

    [Authorize(Roles = "Administrator, Consultant, FinancialAdviser")]
    public IActionResult Index()
    {
        var transactions = _repo.Transaction.GetAllTransactions();
        return View(transactions);
    }

    [HttpGet]
    [Authorize(Roles = "Client")]
    public IActionResult Create()
    {
        var userId = _userManager.GetUserId(User); // Retrieve the logged-in user ID
        var userBankAccount = _repo.BankAccount.FindByCondition(b => b.UserId == userId).FirstOrDefault();

        if (userBankAccount == null)
        {
            return NotFound("User's bank account not found.");
        }

        // Pass the logged-in user's account number to the View using ViewBag
        ViewBag.LoggedInAccountNumber = userBankAccount.AccountNumber;

        ViewBag.TransactionTypes = new List<SelectListItem>
    {
        new SelectListItem { Value = "Transfer", Text = "Transfer" },
        new SelectListItem { Value = "Withdrawal", Text = "Withdrawal" },

    };

        return View();
    }



    // POST: Transaction/Create
    [HttpPost]
    [Authorize(Roles = "Client")] // Authorize clients for Create
    public IActionResult Create(string sourceAccountNumber, string destinationAccountNumber, decimal amount, string transactionType)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var sourceAccount = _repo.BankAccount.GetByAccountNumber(sourceAccountNumber);
                BankAccount destinationAccount = null;

                // If the transaction type is a transfer, get the destination account
                if (transactionType == "Transfer")
                {
                    destinationAccount = _repo.BankAccount.GetByAccountNumber(destinationAccountNumber);

                    if (destinationAccount == null)
                    {
                        ModelState.AddModelError("", "Destination account not found.");
                        return View();
                    }
                }

                // Check if source account exists and has sufficient balance
                if (sourceAccount == null || (transactionType == "Transfer" && sourceAccount.Balance < amount))
                {
                    ModelState.AddModelError("", "One or both accounts not found or insufficient funds.");
                    return View();
                }

                // Handle withdrawals
                if (transactionType == "Withdrawal")
                {
                    // Update balance
                    sourceAccount.Balance -= amount;

                    // Create transaction record
                    var transaction = new Transaction
                    {
                        Amount = amount,
                        TransactionType = transactionType,
                        CreatedAt = DateTime.Now,
                        SourceAccountId = sourceAccount.AccountId,
                        DestinationAccountId = sourceAccount.AccountId // Not needed for withdrawal
                    };

                    // Create notification for the user
                   
                    // Persist the transaction and notification
                    

                    // Prepare the success response
                    ViewBag.WithdrawnAmount = amount;
                    ViewBag.ReceiptNumber = new Random().Next(100000, 999999); // Generate a random receipt number
                    return RedirectToAction("WithdrawalSuccess");
                }
                // Handle transfers
                else if (transactionType == "Transfer")
                {
                    // Update balances
                    sourceAccount.Balance -= amount;
                    destinationAccount.Balance += amount;

                    // Create transaction record
                    var transaction = new Transaction
                    {
                        Amount = amount,
                        TransactionType = transactionType,
                        CreatedAt = DateTime.Now,
                        SourceAccountId = sourceAccount.AccountId,
                        DestinationAccountId = destinationAccount.AccountId
                    };

                    // Create notifications
                    _repo.Transaction.Create(transaction);

                    // Persist the transaction and notifications
                    
                    _repo.Save();

                    return RedirectToAction("TransactionSuccess");
                }
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View();
    }


    [Authorize(Roles = "Administrator, Consultant, FinancialAdviser")] // Allow both clients and administrators to view transaction details
    public IActionResult Details(int id)
    {
        var transaction = _repo.Transaction.GetById(id);

        if (transaction == null)
        {
            return NotFound();
        }

        var sourceAccount = _repo.BankAccount.GetById(transaction.SourceAccountId);
        var destinationAccount = _repo.BankAccount.GetById(transaction.DestinationAccountId);

        if (sourceAccount == null || destinationAccount == null)
        {
            return NotFound(); // Or handle accordingly
        }

        var sourceUser = _repo.User.GetById(sourceAccount.UserId);
        var destinationUser = _repo.User.GetById(destinationAccount.UserId);

        var viewModel = new TransactionDetailViewModel
        {
            SourceName = sourceUser?.FirstName ?? "Unknown",
            SourceSurname = sourceUser?.LastName ?? "Unknown",
            SourceAccountNumber = sourceAccount.AccountNumber,
            ReceiverName = destinationUser?.FirstName ?? "Unknown",
            ReceiverSurname = destinationUser?.LastName ?? "Unknown",
            ReceiverAccountNumber = destinationAccount.AccountNumber,
            CreatedAt = transaction.CreatedAt,
            Amount = transaction.Amount,
            TransactionType = transaction.TransactionType
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Client")] // Authorize clients for TransactionSuccess
    public IActionResult TransactionSuccess()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Client")]
    public IActionResult WithdrawalSuccess()
    {
        return View();
    }

}