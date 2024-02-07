using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Books.DataAccess;
using Books.Application.Services.Contracts;
using Books.Application.Services.Implementation;
using Books.DataAccess.Repositories.Contracts;
using Books.DataAccess.Repositories.Implementation;
using Microsoft.EntityFrameworkCore.Design;

namespace Books.Console.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureSqlContext(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>();  
        }

        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
    }
}
