using Entities.Models;

namespace Contracts
{
    public interface IBookRepository
    {
        void CreateBook(Book book);
        bool CheckDuplicate(string title, string genreName, string authorName, string publisherName);
        IQueryable<Book> GetBook(string name);
    }
}
