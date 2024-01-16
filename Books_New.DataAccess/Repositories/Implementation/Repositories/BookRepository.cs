using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.Linq.Expressions;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateBook(Book book)
        {
            Create(book);
        }

        public bool CheckDuplicate(string title, string genreName, string authorName, string publisherName)
        {
            Expression<Func<Book, bool>> expression = b => b.Title == title && b.Genre.Name == genreName && b.Author.Name == authorName && b.Publisher.Name == publisherName;

            var answer = FindByCondition(expression);

            return answer.IsNullOrEmpty() ? true : false;
        }

        public IQueryable<Book> GetBook(string name)
        {
            Expression<Func<Book, bool>> exception = b => b.Title.Contains(name) || b.Author.Name.Contains(name);
            var bookList = FindByCondition(exception).Include(b => b.Author);

            return bookList;
        }
    }
}
