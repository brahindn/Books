using Books.Domain.Entities;

namespace Books.Application.Services.Contracts.Services
{
    public interface IBookService
    {
        Task CreateBookAsync(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        Task<IQueryable<Book>> GetFilteredBooksAsync(FilterConditions filterConditions);
    }
}