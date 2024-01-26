using Books.Domain;

namespace Books.Application
{
    public interface IAuthorService
    {
        Task CreateAuthorAsync(string authorName);

        Task<Author> GetAuthorAsync(string name);
    }
}
