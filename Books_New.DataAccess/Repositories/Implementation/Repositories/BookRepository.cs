using Books_New.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Books_New.DataAccess
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

            var answer = FindByCondition(expression);

            return answer.IsNullOrEmpty() ? true : false;
        }

        public IQueryable<Book> GetBook(string stringData)
        {
            Expression<Func<Book, bool>> exception = b =>
            b.Title.Contains(stringData) || b.Author.Name.Contains(stringData) || 
            b.Genre.Name.Contains(stringData) || b.Publisher.Name.Contains(stringData);

            var bookList = FindByCondition(exception).Include(b => b.Author);

            return bookList;
        }

        public IQueryable<Book> GetBook(int pages)
        {
            Expression<Func<Book, bool>> exception = b => b.Pages.Value == pages;

            var bookList = FindByCondition(exception).Include(b => b.Author);

            return bookList;
        }

        public IQueryable<Book> GetBook(DateTime realiseDate)
        {
            Expression<Func<Book, bool>> exception = b => b.ReleaseDate.Value == realiseDate;

            var bookList = FindByCondition(exception).Include(b => b.Author);

            return bookList;
        }
    }
}
