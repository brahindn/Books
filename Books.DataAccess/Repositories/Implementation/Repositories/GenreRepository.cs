using Books.Domain;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        {
        }

        public async Task<Genre> GetGenreAsync(string name)
        {
            return await FindByConditionAsync(g => g.Name == name).SingleOrDefaultAsync();
        }
    }
}
