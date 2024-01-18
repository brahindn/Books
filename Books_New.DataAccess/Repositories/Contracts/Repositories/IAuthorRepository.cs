using Books_New.Entities;

namespace Books_New.DataAccess
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Author GetAuthor(string name);
    }
}
