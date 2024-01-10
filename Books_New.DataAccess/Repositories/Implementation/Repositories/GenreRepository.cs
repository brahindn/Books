using Contracts;
using Entities.Models;
using Repository;

namespace Books_New.DataAccess.Repositories.Implementation.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateGenre(Genre genre)
        {
            Create(genre);
        }

        public Genre GetGenre(string name)
        {
            return FindByCondition(g => g.Name == name).SingleOrDefault();
        }
    }
}
