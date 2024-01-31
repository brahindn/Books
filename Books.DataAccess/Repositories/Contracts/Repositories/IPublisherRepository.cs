using Books.Domain.Entities;

namespace Books.DataAccess.Repositories.Contracts.Repositories
{
    public interface IPublisherRepository
    {
        void Create(Publisher publisher);
        Task<Publisher> GetPublisherAsync(string name);
    }
}
