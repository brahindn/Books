using Books.Domain.Entities;

namespace Books.Application.Services.Contracts.Services
{
    public interface IAuthorService
    {
        Task CreateAuthorAsync(string authorName);

        Task<Author> GetAuthorAsync(string name);
    }
}
