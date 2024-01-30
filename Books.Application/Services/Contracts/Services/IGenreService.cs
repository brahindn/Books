using Books.Domain;

namespace Books.Application
{
    public interface IGenreService
    {
        Task CreateGenreAsync(string genreName);
        Task<Genre> GetGenreAsync(string name);
    }
}
