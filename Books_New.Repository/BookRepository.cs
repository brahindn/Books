using Contracts;
using Entities.Models;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) { }
    }
}
