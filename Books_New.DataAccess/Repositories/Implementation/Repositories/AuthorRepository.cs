using Contracts;
using Entities.Models;
using Repository;
using System.Runtime.InteropServices;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateAuthor(Author author)
        {
            Create(author);
        }
    }
}
