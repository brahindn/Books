using Books.Domain;

namespace Books.Application
{
    public interface IBookService
    {
        void CreateBook(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        IQueryable<Book> GetBook(string name);
        IQueryable<Book> GetBook(int pages);
        IQueryable<Book> GetBook(DateTime releaseDate);
    }
}
