using Entities.Models;

namespace Books_New.Application.Services.Contracts.Services
{
    public interface IGenreService
    {
        void CreateGenre(string field);
        Genre GetGenre(string name);
    }
}
