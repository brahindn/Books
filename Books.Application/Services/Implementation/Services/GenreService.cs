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

        public async Task CreateGenreAsync(string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                return;
            }

            var genre = new Genre { Name = genreName };
            _repositoryManager.Genre.Create(genre);
            await _repositoryManager.SaveAsync();
        }

        public async Task<Genre> GetGenreAsync(string name)
        {
            return await _repositoryManager.Genre.GetGenreAsync(name);
        }
    }
}
