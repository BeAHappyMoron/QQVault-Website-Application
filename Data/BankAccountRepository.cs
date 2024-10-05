using QQVault.Models;

namespace QQVault.Data
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
            
        }

        public BankAccount GetByAccountNumber(string accountNumber)
        {
            return _bankDbContext.BankAccounts
                .FirstOrDefault(a => a.AccountNumber == accountNumber);
        }
    }
}
