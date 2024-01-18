using Books_New.Entities;

namespace Books_New.DataAccess
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        Genre GetGenre(string name);
    }
}
