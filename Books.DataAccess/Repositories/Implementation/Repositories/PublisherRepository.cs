using Books.DataAccess.Repositories.Contracts.Repositories;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Repositories.Implementation.Repositories
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
