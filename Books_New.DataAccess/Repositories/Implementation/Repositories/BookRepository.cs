using Contracts;
using Entities.Models;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }
    }
}
