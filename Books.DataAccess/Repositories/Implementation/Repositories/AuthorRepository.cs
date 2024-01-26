using Books.Domain;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Author> GetAuthorAsync(string name)
        {
            return await FindByConditionAsync(a => a.Name == name).SingleOrDefaultAsync();
        }
    }
}