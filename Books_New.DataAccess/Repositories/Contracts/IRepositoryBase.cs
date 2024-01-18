using System.Linq.Expressions;

namespace Books_New.DataAccess
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
    }
}
