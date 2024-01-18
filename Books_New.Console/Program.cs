using Books_New.Application;
using Books_New.Console;
using Books_New.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

IConfiguration config = builder.Build();

var services = new ServiceCollection();

services.ConfigureRepositoryManager();
services.ConfigureServiceManager();
services.ConfigureSqlContext(config);

var serviceProvider = services.BuildServiceProvider();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

PopulateDatabaseFromFile(path, serviceProvider);

var filterConditions = new FilterConditions()
{
    BookName = config.GetSection("FilterConditions:BookName").Get<List<string>>(),
    AuthorName = config.GetSection("FilterConditions:AuthorName").Get<List<string>>()
};

var filterBookList = SearchingBook(filterConditions.BookName, serviceProvider);
Console.WriteLine("Data filtered by Title:");
ShowBooksWithFilter(filterBookList);

var filterAuthorList = SearchingBook(filterConditions.AuthorName, serviceProvider);
Console.WriteLine("Data filtered by Author:");
ShowBooksWithFilter(filterAuthorList);


void ShowBooksWithFilter(List<Book> list)
{
    foreach (var entity in list)
    {
        Console.WriteLine($"{entity.Title} - {entity.Author.Name}");
    }

    Console.WriteLine();
    Console.WriteLine("Press something to continue..." + "\n");
    Console.ReadKey();
}

List<Book> SearchingBook(List<string> filterConditions, ServiceProvider serviceProvider)
{
    var result = new List<Book>();
    var serviceManager = serviceProvider.GetRequiredService<IServiceManager>();
    
    foreach (var bookName in filterConditions)
    {
        result.AddRange(serviceManager.BookService.GetBook(bookName.ToString()));
    }

    return result;
}

void PopulateDatabaseFromFile(string path, ServiceProvider servicesProvider)
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

    Console.WriteLine();
    Console.WriteLine("Press something to continue..." + "\n");
    Console.ReadKey();
}

bool DataCheck(string[] fields)
{
    return fields[0] is string && int.TryParse(fields[1], out int page) && fields[2] is string && DateTime.TryParse(fields[3], out DateTime time) && fields[4] is string && fields[5] is string;
}
