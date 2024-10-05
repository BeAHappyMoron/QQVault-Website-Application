using Microsoft.EntityFrameworkCore;
using QQVault.Models;

namespace QQVault.Data
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly IBankAccountRepository _bankAccountRepo;
        public TransactionRepository(BankDbContext bankDbContext, IBankAccountRepository bankAccountRepo) : base(bankDbContext)
        {
            _bankAccountRepo = bankAccountRepo;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _bankDbContext.Transactions
                .Include(t => t.SourceAccount)
                .Include(t => t.DestinationAccount)
                .ToList();
        }

        public IEnumerable<Transaction> GetUserTransactions(string userId)
        {
            return _bankDbContext.Transactions
                .Include(t => t.SourceAccount) // Ensure related SourceAccount is loaded
                .Include(t => t.DestinationAccount) // Ensure related DestinationAccount is loaded
                .Where(t => t.SourceAccount.UserId == userId || t.DestinationAccount.UserId == userId)
                .ToList(); // Fetch the result as a list

        }
    }
}