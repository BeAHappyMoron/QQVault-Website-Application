using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace QQVault.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BankDbContext _bankDbContext;

        public RepositoryBase(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public void Create(T entity)
        {
            _bankDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _bankDbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _bankDbContext.Set<T>().Update(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _bankDbContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _bankDbContext.Set<T>().Where(expression);
        }

        public T GetById(object id)
        {
            return _bankDbContext.Set<T>().Find(id);
        }
    }
}
