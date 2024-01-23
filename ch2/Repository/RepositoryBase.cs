using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext context)
        {
            RepositoryContext = context;
        }

        // EF
        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (trackChanges)
                return RepositoryContext.Set<T>();
            return RepositoryContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
                return RepositoryContext.Set<T>().Where(expression);
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Add(T entity)
            => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity)
            => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity)
            => RepositoryContext.Set<T>().Remove(entity);
    }
}
