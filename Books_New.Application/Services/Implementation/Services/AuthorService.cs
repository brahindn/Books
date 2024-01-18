using Books_New.DataAccess;
using Books_New.Entities;

namespace Books_New.Application
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AuthorService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreateAuthor(string authorName)
        {
            if(string.IsNullOrEmpty(authorName))
            {
                return;
            }

            var author = new Author { Name = authorName };
            _repositoryManager.Author.Create(author);
            _repositoryManager.Save();
        }

        public Author GetAuthor(string name)
        {
            return _repositoryManager.Author.GetAuthor(name);
        }
    }
}
