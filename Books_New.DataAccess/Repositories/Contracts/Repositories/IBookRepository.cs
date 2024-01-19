using Books_New.Entities;

namespace Books_New.DataAccess
{
    public interface IBookRepository
    {
        void Create(Book book);
        bool CheckDuplicate(string title, string genreName, string authorName, string publisherName);
        IQueryable<Book> GetBook(string date);
        IQueryable<Book> GetBook(int pages);
        IQueryable<Book> GetBook(DateTime realiseDate);
    }
}
