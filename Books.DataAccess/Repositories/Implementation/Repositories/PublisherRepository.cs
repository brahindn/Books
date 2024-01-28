using Books.Domain;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess
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
