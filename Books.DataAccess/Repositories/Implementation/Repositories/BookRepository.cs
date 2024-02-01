using Books.DataAccess.Repositories.Contracts.Repositories;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Books.DataAccess.Repositories.Implementation.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public bool CheckDuplicate(string title, string genreName, string authorName, string publisherName)
        {
            Expression<Func<Book, bool>> expression = b => b.Title == title && b.Genre.Name == genreName && b.Author.Name == authorName && b.Publisher.Name == publisherName;

            return FindByCondition(expression).IsNullOrEmpty();
        }

        public IQueryable<Book> GetAllBooks()
        {
            return FindAll();
        }

        public async Task<IQueryable<Book>> GetFilteredBooksAsync(IQueryable<Book> query)
        {
            Expression<Func<Book, bool>> expression = b => query.Contains(b);
            return FindByCondition(expression);
        }
    }
}
