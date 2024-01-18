using Books_New.Entities;

namespace Books_New.Application
{
    public interface IBookService
    {
        void CreateBook(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        IQueryable<Book> GetBook(string name);
    }
}
