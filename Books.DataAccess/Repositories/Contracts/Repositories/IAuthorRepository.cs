using Books.Domain;

namespace Books.DataAccess
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Author GetAuthor(string name);
    }
}
