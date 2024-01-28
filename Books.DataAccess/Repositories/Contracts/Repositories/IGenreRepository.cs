using Books.Domain;

namespace Books.DataAccess
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        Genre GetGenre(string name);
    }
}
