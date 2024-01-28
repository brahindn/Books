using Books.Domain;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Author GetAuthor(string name)
        {
            return FindByCondition(a => a.Name == name).SingleOrDefault();
        }
    }
}