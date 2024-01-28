using Books.Domain;

namespace Books.Application
{
    public interface IGenreService
    {
        void CreateGenre(string genreName);
        Genre GetGenre(string name);
    }
}
