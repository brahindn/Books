using Books.DataAccess;
using Books.Domain;

namespace Books.Application
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
            if (string.IsNullOrEmpty(publisherName))
            {
                return;
            }

            var publisher = new Publisher { Name = publisherName };
            _repositoryManager.Publisher.Create(publisher);
            await _repositoryManager.SaveAsync();
        }

        public async Task<Publisher> GetPublisherAsync(string name)
        {
            return await _repositoryManager.Publisher.GetPublisherAsync(name);
        }
    }
}
