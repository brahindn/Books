using Contracts;
using Entities.Models;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }
    }
}
