using Books.Domain.Entities;

namespace Books.Application.Services.Contracts.Services
{
    public interface IGenreService
    {
        Task CreateGenreAsync(string genreName);
        Task<Genre> GetGenreAsync(string name);
    }
}
