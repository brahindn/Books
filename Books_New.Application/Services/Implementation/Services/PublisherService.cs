using Books_New.Application.Services.Contracts.Services;
using Contracts;

namespace Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PublisherService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }
    }
}
