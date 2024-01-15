using Books_New.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var services = new ServiceCollection();

services.ConfigureRepositoryManager();
services.ConfigureServiceManager();
services.ConfigureSqlContext(configuration);

var serviceProvider = services.BuildServiceProvider();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

Update(path, serviceProvider);

void Update(string path, ServiceProvider servicesProvider)
{
    while (!File.Exists(path))
    {
        Console.WriteLine("Specify a valid file path or press C to exit.");

        path = Console.ReadLine();

        if (path.ToUpper() == "C")
        {
            Environment.Exit(0);
        }
    }
    if (path != null && File.Exists(path))
    {
        Console.Write("File detected\n");
        string[] lines = File.ReadAllLines(path);
        var serviceManager = servicesProvider.GetService<IServiceManager>();

        foreach (var line in lines)
        {
            string[] fields = line.Split(',');

            if (DataCheck(fields))
            {
                try
                {
                    serviceManager.BookService.CreateBook(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
                }
                catch
                {
                    Console.WriteLine("(Something went wrong)");
                }
            }
        }
    }
}

bool DataCheck(string[] fields)
{
    if (fields[0] is string && int.TryParse(fields[1], out int page) && fields[2] is string && DateTime.TryParse(fields[3], out DateTime time) && fields[4] is string && fields[5] is string)
    {
        return true;
    }

    return false;
}



