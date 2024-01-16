using Entities.Models;

namespace Books_New.Application.Services.Contracts.Services
{
    public interface IBookService
    {
        void CreateBook(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        IQueryable<Book> GetBook(string name);
    }
}
