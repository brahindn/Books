using Books.DataAccess.Repositories.Contracts.Repositories;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Repositories.Implementation.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<Genre> GetGenreAsync(string name)
        {
            return await FindByCondition(g => g.Name == name).SingleOrDefaultAsync();
        }
    }
}
