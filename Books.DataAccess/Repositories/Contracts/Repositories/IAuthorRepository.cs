using Books.Domain.Entities;

namespace Books.DataAccess.Repositories.Contracts.Repositories
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Task<Author> GetAuthorAsync(string name);
    }
}
