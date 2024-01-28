using Books.DataAccess;
using Books.Domain;

namespace Books.Application
{
    public class GenreService : IGenreService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GenreService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreateGenre(string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                return;
            }

            var genre = new Genre { Name = genreName };
            _repositoryManager.Genre.Create(genre);
            _repositoryManager.Save();
        }

        public Genre GetGenre(string name)
        {
            return _repositoryManager.Genre.GetGenre(name);
        }
    }
}
