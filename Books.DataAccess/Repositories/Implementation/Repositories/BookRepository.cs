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

        public async Task<IQueryable<Book>> GetBookAsync(string stringData)
        {
            Expression<Func<Book, bool>> expression = b =>
            b.Title.Contains(stringData) || b.Author.Name.Contains(stringData) ||
            b.Genre.Name.Contains(stringData) || b.Publisher.Name.Contains(stringData);

            return FindByCondition(expression).Include(b => b.Author);
        }

        public async Task<IQueryable<Book>> GetBookAsync(int pages)
        {
            Expression<Func<Book, bool>> expression = b => b.Pages.Value == pages;

            return FindByCondition(expression).Include(b => b.Author);
        }

        public async Task<IQueryable<Book>> GetBookAsync(DateTime releaseDate)
        {
            Expression<Func<Book, bool>> expression = b => b.ReleaseDate.Value == releaseDate;

            return FindByCondition(expression).Include(b => b.Author);
        }
    }
}
