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

        public async Task<Publisher> GetPublisherAsync(string name)
        {
            return await FindByCondition(p => p.Name == name).SingleOrDefaultAsync();
        }
    }
}
