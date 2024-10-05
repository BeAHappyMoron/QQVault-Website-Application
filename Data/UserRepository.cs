using QQVault.Models;

namespace QQVault.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {

        }
    }
}
