using Books.Domain.Entities;

namespace Books.Application.Services.Contracts.Services
{
    public interface IPublisherService
    {
        Task CreatePublisherAsync(string publisherName);
        Task<Publisher> GetPublisherAsync(string name);
    }
}
