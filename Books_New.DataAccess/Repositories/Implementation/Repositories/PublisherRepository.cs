using Books_New.Entities;

namespace Books_New.DataAccess
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        {
        }

        public Publisher GetPublisher(string name)
        {
            return FindByCondition(p => p.Name == name).SingleOrDefault();
        }
    }
}
