using Contracts;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Update(T entity)
        {
            RepositoryContext.Add(entity);
        }
    }
}
