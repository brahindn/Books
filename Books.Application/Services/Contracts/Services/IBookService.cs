using Books.Domain.Entities;

namespace Books.Application.Services.Contracts.Services
{
    public interface IBookService
    {
        Task CreateBookAsync(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        IQueryable<Book> GetFilteredBooksAsync(FilterConditions filterConditions);
    }
}