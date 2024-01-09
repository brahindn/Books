namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        void Update(T entity);
    }
}
