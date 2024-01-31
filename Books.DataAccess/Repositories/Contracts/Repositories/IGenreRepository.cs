using Books.Domain.Entities;

namespace Books.DataAccess.Repositories.Contracts.Repositories
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        Task<Genre> GetGenreAsync(string name);
    }
}
