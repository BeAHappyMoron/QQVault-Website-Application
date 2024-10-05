using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QQVault.Models;
using QQVault.Data;
using System.Linq;
using System.Threading.Tasks;
using QQVault.Models.ViewModels;

namespace QQVault.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly UserManager<User> _userManager;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionRepository _transactionRepo;

        public DashboardController(UserManager<User> userManager,
            IBankAccountRepository bankAccountRepository,
            ITransactionRepository transactionRepo,
            IRepositoryWrapper repo)
        {
            _userManager = userManager;
            _bankAccountRepository = bankAccountRepository;
            _transactionRepo = transactionRepo;
            _repo = repo;
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ClientDashboard()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the user's bank account
            var bankAccount = _bankAccountRepository.FindByCondition(b => b.UserId == user.Id).FirstOrDefault();
            if (bankAccount == null)
            {
                return NotFound("Bank account not found.");
            }

            // Get the user's transactions
            var transactions = _transactionRepo.GetUserTransactions(user.Id).ToList();

            // Create the ViewModel
            var model = new DashboardViewModel
            {
                User = user,
                BankAccount = bankAccount,
                Transactions = transactions,
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator, Consultant, FinancialAdviser")]
        public async Task<IActionResult> AdminDashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get all transactions (admin view)
            var transactions = _transactionRepo.GetAllTransactions().ToList();

            // Calculate total money transferred and withdrawn
            var totalMoneyTransferred = transactions
                .Where(t => t.TransactionType == "Transfer")
                .Sum(t => t.Amount);

            var totalMoneyWithdrawn = transactions
                .Where(t => t.TransactionType == "Withdraw")
                .Sum(t => t.Amount);

            // Get total number of transactions
            var totalTransactions = transactions.Count();

            // Create the ViewModel
            var model = new AdminDashboardViewModel
            {
                User = user,
                AllTransactions = transactions,
                TotalMoneyTransferred = totalMoneyTransferred,
                TotalMoneyWithdrawn = totalMoneyWithdrawn,
                TotalTransactions = totalTransactions
            };

            return View(model);
        }
    }
}
