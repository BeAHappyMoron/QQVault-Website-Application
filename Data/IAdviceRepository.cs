using QQVault.Models;

namespace QQVault.Data
{
    public interface IAdviceRepository : IRepositoryBase<Advice>
    {
        IEnumerable<Advice> GetAdvicesByUserId(string userId);
    }
}

