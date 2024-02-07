using Books.Application;
using Books.Console.Extensions;
using Books.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Console
{
    public class Startup
    {
        public FilterConditions FilterConditions { get; set; }
        public IConfiguration Config { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            Config = builder.Build();

            FilterConditions = Config.GetSection("FilterConditions").Get<FilterConditions>()!;
        }

       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            services.ConfigureSqlContext();
        }
    }
}
