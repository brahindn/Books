using Entities.Models;

namespace Contracts
{
    public interface IGenreRepository
    {
        void CreateGenre(Genre genre);
        Genre GetGenre(string name);
    }
}
