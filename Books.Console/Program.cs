using Books.Application;
using Books.Application.Services.Contracts;
using Books.Console;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var startup = new Startup();
var services = new ServiceCollection();

startup.ConfigerationServices(services);

var serviceProvider = services.BuildServiceProvider();
var serviceManager = serviceProvider.GetService<IServiceManager>();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

await PopulateDatabaseFromFile(path);

var filterConditions = new FilterConditions()
{
    BookName = startup.FilterConditions.BookName,
    AuthorName = startup.FilterConditions.AuthorName,
    GenreName = startup.FilterConditions.GenreName,
    PublisherName = startup.FilterConditions.PublisherName,
    PageNumber = startup.FilterConditions.PageNumber,
    ReleaseDate = startup.FilterConditions.ReleaseDate
};

var filteredBooks = await SearchingBook(filterConditions);

foreach(var book in filteredBooks)
{
    Console.WriteLine($"{book.Title}");
}

async Task<List<Book>> SearchingBook(FilterConditions filterConditions)
{
    var filteredBooks = await serviceManager.BookService.GetFilteredBooksAsync(filterConditions);
    return await filteredBooks.ToListAsync();
}

async Task PopulateDatabaseFromFile(string path)
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

        foreach (var line in lines)
        {
            string[] fields = line.Split(',');
            string errorFileName = $"{DateTime.Now:yyyyMMdd_HHmm}.txt";
            string errorFilePath = Path.Combine(Directory.GetCurrentDirectory(), errorFileName);

            if (DataCheck(fields))
            {
                try
                {
                    await serviceManager.BookService.CreateBookAsync(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                    File.AppendAllText(errorFilePath, line + Environment.NewLine);
                }
            }
            else
            {
                File.AppendAllText(errorFilePath, line + Environment.NewLine);
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine("Press something to continue..." + "\n");
    Console.ReadKey();
}

bool DataCheck(string[] fields)
{
    if (fields[1].Equals("Pages", StringComparison.OrdinalIgnoreCase))
    {
    return false;
    }

    return fields[0] is string && fields[2] is string && fields[4] is string && fields[5] is string;
}
