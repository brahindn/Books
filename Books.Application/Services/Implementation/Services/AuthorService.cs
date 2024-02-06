using Books.Application.Services.Contracts.Services;
using Books.DataAccess.Repositories.Contracts;
using Books.Domain.Entities;

namespace Books.Application.Services.Implementation.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AuthorService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public async Task CreateAuthorAsync(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                return;
            }

            var author = new Author { Name = authorName };
            _repositoryManager.Author.Create(author);
            await _repositoryManager.SaveAsync();
        }

        public Task<Author> GetAuthorAsync(string name)
        {
            return _repositoryManager.Author.GetAuthorAsync(name);
        }
    }
}
