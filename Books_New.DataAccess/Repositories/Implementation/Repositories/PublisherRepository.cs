using Contracts;
using Entities.Models;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreatePublisher(Publisher publisher)
        {
            Create(publisher);
        }

        public Publisher GetPublisher(string name)
        {
            return FindByCondition(p => p.Name == name).SingleOrDefault();
        }
    }
}
