using Books.Application.Services.Contracts.Services;
using Books.DataAccess.Repositories.Contracts;
using Books.Domain.Entities;

namespace Books.Application.Services.Implementation.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PublisherService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public async Task CreatePublisherAsync(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                return;
            }

            var publisher = new Publisher { Name = publisherName };
            _repositoryManager.Publisher.Create(publisher);
            await _repositoryManager.SaveAsync();
        }

        public Task<Publisher> GetPublisherAsync(string name)
        {
            return _repositoryManager.Publisher.GetPublisherAsync(name);
        }
    }
}
