using Books_New.Entities;

namespace Books_New.Application
{
    public interface IGenreService
    {
        void CreateGenre(string genreName);
        Genre GetGenre(string name);
    }
}
