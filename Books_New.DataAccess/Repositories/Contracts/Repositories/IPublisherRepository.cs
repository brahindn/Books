using Entities.Models;

namespace Contracts
{
    public interface IPublisherRepository
    {
        void CreatePublisher(Publisher publisher);
        Publisher GetPublisher(string name);
    }
}
