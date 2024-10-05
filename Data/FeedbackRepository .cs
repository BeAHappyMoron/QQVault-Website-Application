using Microsoft.EntityFrameworkCore;
using QQVault.Models;

namespace QQVault.Data
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
        }

        public IEnumerable<Feedback> FindAllWithUsers()
        {
            return _bankDbContext.Feedbacks.Include(f => f.User).ToList();
        }
    }
}
