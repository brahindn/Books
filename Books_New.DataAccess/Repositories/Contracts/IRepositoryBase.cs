namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        void Update(T entity);
    }
}
