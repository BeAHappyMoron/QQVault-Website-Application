using QQVault.Models;

namespace QQVault.Data
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetUserTransactions(string userId);

    }
}
