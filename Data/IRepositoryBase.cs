using System.Linq.Expressions;

namespace QQVault.Data
{
    public interface IRepositoryBase<T>
    {
        T GetById(object id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
