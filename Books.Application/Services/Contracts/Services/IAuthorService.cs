using Books.Domain;

namespace Books.Application
{
    public interface IAuthorService
    {
        void CreateAuthor(string authorName);

        Author GetAuthor(string name);
    }
}
