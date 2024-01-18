using Books_New.Entities;

namespace Books_New.DataAccess
{
    public interface IPublisherRepository
    {
        void Create(Publisher publisher);
        Publisher GetPublisher(string name);
    }
}
