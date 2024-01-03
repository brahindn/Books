using Books_New.Application.Services.Contracts.Services;
using Contracts;

namespace Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepositoryManager _repositoryManager;

        public GenreService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }
    }
}
