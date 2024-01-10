using Entities.Models;

namespace Books_New.Application.Services.Contracts.Services
{
    public interface IAuthorService
    {
        void CreateAuthor(string field);

        Author GetAuthor(string name);
    }
}
