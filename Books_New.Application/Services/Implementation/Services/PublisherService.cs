using Books_New.DataAccess;
using Books_New.Entities;

namespace Books_New.Application
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PublisherService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreatePublisher(string publisherName)
        {
            if (string.IsNullOrEmpty(publisherName))
            {
                return;
            }

            var publisher = new Publisher { Name = publisherName };
            _repositoryManager.Publisher.Create(publisher);
            _repositoryManager.Save();
        }

        public Publisher GetPublisher(string name)
        {
            return _repositoryManager.Publisher.GetPublisher(name);
        }
    }
}
