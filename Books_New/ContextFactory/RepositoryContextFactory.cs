using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Repository;

namespace Books_New.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        private readonly IConfiguration _configuration;
        public RepositoryContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public RepositoryContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(_configuration.GetConnectionString("sqlConnection"));

            return new RepositoryContext(builder.Options);
        }
    }
}
