using System.Linq.Expressions;

namespace Books.DataAccess.Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAll();
        void Create(T entity);
    }
}
