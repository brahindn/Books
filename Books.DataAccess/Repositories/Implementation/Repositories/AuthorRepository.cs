using Books.DataAccess.Repositories.Contracts.Repositories;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Repositories.Implementation.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Author> GetAuthorAsync(string name)
        {
            return await FindByCondition(a => a.Name == name).SingleOrDefaultAsync();
        }
    }
}