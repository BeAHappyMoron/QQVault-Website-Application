using QQVault.Models;

namespace QQVault.Data
{
    public interface IBankAccountRepository : IRepositoryBase<BankAccount>
    {
        BankAccount GetByAccountNumber(string accountNumber);
    }


}
