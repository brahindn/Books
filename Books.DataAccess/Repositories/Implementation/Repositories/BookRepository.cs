using Books.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Books.DataAccess
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

            return FindByConditionAsync(expression).IsNullOrEmpty();
        }

        public async Task<IQueryable<Book>> GetBookAsync(string stringData)
        {
            Expression<Func<Book, bool>> expression = b =>
            b.Title.Contains(stringData) || b.Author.Name.Contains(stringData) || 
            b.Genre.Name.Contains(stringData) || b.Publisher.Name.Contains(stringData);

            return FindByConditionAsync(expression).Include(b => b.Author);
        }

        public async Task<IQueryable<Book>> GetBookAsync(int pages)
        {
            Expression<Func<Book, bool>> expression = b => b.Pages.Value == pages;

            return FindByConditionAsync(expression).Include(b => b.Author);
        }

        public async Task<IQueryable<Book>> GetBookAsync(DateTime releaseDate)
        {
            Expression<Func<Book, bool>> expression = b => b.ReleaseDate.Value == releaseDate;

            return FindByConditionAsync(expression).Include(b => b.Author);
        }
    }
}
