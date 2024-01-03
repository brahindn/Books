using Contracts;
using Entities.Models;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
