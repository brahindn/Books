using Books_New.Entities;

namespace Books_New.Application
{
    public interface IAuthorService
    {
        void CreateAuthor(string authorName);

        Author GetAuthor(string name);
    }
}
