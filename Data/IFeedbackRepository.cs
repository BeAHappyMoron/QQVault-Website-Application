using QQVault.Models;

namespace QQVault.Data
{
    public interface IFeedbackRepository : IRepositoryBase<Feedback>
    {
        IEnumerable<Feedback> FindAllWithUsers();
    }
}
