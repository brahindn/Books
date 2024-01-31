using Books.Domain.Entities;

namespace Books.DataAccess.Repositories.Contracts.Repositories
{
    public interface IBookRepository
    {
        void Create(Book book);
        bool CheckDuplicate(string title, string genreName, string authorName, string publisherName);
        Task<IQueryable<Book>> GetBookAsync(string date);
        Task<IQueryable<Book>> GetBookAsync(int pages);
        Task<IQueryable<Book>> GetBookAsync(DateTime releaseDate);
    }
}
