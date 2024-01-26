using Books.DataAccess;
using Books.Domain;

namespace Books.Application
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
            if(string.IsNullOrEmpty(authorName))
            {
                return;
            }

            var author = new Author { Name = authorName };
            _repositoryManager.Author.Create(author);
            await _repositoryManager.SaveAsync();
        }

        public async Task<Author> GetAuthorAsync(string name)
        {
            return await _repositoryManager.Author.GetAuthorAsync(name);
        }
    }
}
