using Books.Domain;

namespace Books.Application
{
    public interface IBookService
    {
        Task CreateBookAsync(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        Task<List<Book>> GetBooksAsync(FilterConditions filterConditions);
    }
}
