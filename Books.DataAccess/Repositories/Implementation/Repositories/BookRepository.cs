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

            return FindByCondition(expression).IsNullOrEmpty();
        }

        public IQueryable<Book> GetBook(string stringData)
        {
            Expression<Func<Book, bool>> expression = b =>
            b.Title.Contains(stringData) || b.Author.Name.Contains(stringData) || 
            b.Genre.Name.Contains(stringData) || b.Publisher.Name.Contains(stringData);

            return FindByCondition(expression).Include(b => b.Author);
        }

        public IQueryable<Book> GetBook(int pages)
        {
            Expression<Func<Book, bool>> expression = b => b.Pages.Value == pages;

            return FindByCondition(expression).Include(b => b.Author);
        }

        public IQueryable<Book> GetBook(DateTime releaseDate)
        {
            Expression<Func<Book, bool>> expression = b => b.ReleaseDate.Value == releaseDate;

            return FindByCondition(expression).Include(b => b.Author);
        }
    }
}
