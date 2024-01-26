using Books.Domain;

namespace Books.DataAccess
{
    public interface IPublisherRepository
    {
        void Create(Publisher publisher);
        Task<Publisher> GetPublisherAsync(string name);
    }
}
