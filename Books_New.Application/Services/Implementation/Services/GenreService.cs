using Books_New.Application.Services.Contracts.Services;
using Contracts;
using Entities.Models;

namespace Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GenreService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreateGenre(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return;
            }

            var genre = new Genre { Name = field };
            _repositoryManager.Genre.CreateGenre(genre);
            _repositoryManager.Save();
        }

        public Genre GetGenre(string name)
        {
            return _repositoryManager.Genre.GetGenre(name);
        }
    }
}
