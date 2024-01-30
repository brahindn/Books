using Books.Domain;

namespace Books.DataAccess
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Task<Author> GetAuthorAsync(string name);
    }
}
