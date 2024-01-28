using Books.Domain;

namespace Books.Application
{
    public interface IPublisherService
    {
        void CreatePublisher(string publisherName);
        Publisher GetPublisher(string name);
    }
}
