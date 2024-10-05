using QQVault.Models;

namespace QQVault.Data
{
    public class AdviceRepository : RepositoryBase<Advice>, IAdviceRepository
    {
        public AdviceRepository(BankDbContext bankDbContext) : base(bankDbContext) { }

        public IEnumerable<Advice> GetAdvicesByUserId(string userId)
        {
            return _bankDbContext.Advices
                .Where(a => a.UserId == userId)
                .ToList();
        }
    }

}
