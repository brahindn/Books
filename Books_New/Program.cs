using Books_New.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

var builder = new ConfigurationBuilder()
    .SetBasePath (Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builder.Build();
var services = new ServiceCollection();

services.ConfigureRepositoryManager();
services.ConfigureServiceManager();
services.ConfigureSqlContext(configuration);

var serviceProvider = services.BuildServiceProvider();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

var repositoryBase = new RepositoryBase(File);






