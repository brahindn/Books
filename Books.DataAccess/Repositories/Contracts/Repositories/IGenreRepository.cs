using Books.Domain;

namespace Books.DataAccess
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        Task<Genre> GetGenreAsync(string name);
    }
}
