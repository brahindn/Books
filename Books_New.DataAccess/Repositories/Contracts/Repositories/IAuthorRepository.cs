using Entities.Models;

namespace Contracts
{
    public interface IAuthorRepository
    {
        void CreateAuthor(Author author);
        Author GetAuthor(string name);
    }
}
