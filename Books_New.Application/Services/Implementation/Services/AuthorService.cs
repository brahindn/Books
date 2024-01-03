using Books_New.Application.Services.Contracts.Services;
using Contracts;

namespace Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AuthorService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }
    }
}
