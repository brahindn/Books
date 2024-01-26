using Books.Domain;

namespace Books.Application
{
    public interface IPublisherService
    {
        Task CreatePublisherAsync(string publisherName);
        Task<Publisher> GetPublisherAsync(string name);
    }
}
