using Books_New.Application.Services.Contracts.Services;
using Contracts;
using Entities.Models;

namespace Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AuthorService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreateAuthor(string field)
        {
            var author = new Author { Name = field };
            _repositoryManager.Author.CreateAuthor(author);
            _repositoryManager.Save();
        }

        public Author GetAuthor(string name)
        {
            return _repositoryManager.Author.GetAuthor(name);
        }
    }
}
