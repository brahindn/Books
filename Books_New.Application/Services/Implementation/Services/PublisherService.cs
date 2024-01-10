using Books_New.Application.Services.Contracts.Services;
using Contracts;
using Entities.Models;

namespace Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PublisherService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreatePublisher(string field)
        {
            var publisher = new Publisher { Name = field };
            _repositoryManager.Publisher.CreatePublisher(publisher);
            _repositoryManager.Save();
        }

        public Publisher GetPublisher(string name)
        {
            return _repositoryManager.Publisher.GetPublisher(name);
        }
    }
}
