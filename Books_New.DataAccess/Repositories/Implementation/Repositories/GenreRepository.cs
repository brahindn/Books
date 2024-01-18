using Books_New.Entities;

namespace Books_New.DataAccess
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        {
        }

        public Genre GetGenre(string name)
        {
            return FindByCondition(g => g.Name == name).SingleOrDefault();
        }
    }
}
