using Books_New.Entities;

namespace Books_New.Application
{
    public interface IPublisherService
    {
        void CreatePublisher(string publisherName);
        Publisher GetPublisher(string name);
    }
}
