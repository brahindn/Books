using Books.Domain;

namespace Books.Application
{
    public interface IBookService
    {
        Task CreateBookAsync(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName);
        Task<IQueryable<Book>> GetBookAsync(string name);
        Task<IQueryable<Book>> GetBookAsync(int pages);
        Task<IQueryable<Book>> GetBookAsync(DateTime releaseDate);
    }
}
