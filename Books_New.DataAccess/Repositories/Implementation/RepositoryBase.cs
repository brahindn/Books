using Contracts;
using Repository;
using System.Linq.Expressions;

namespace Books_New.DataAccess.Repositories.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression);
        }
          

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }
    }
}
