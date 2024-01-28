using Books.Domain;

namespace Books.DataAccess
{
    public interface IPublisherRepository
    {
        void Create(Publisher publisher);
        Publisher GetPublisher(string name);
    }
}
