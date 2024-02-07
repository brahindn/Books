using Books.Application;
using Books.Application.Services.Contracts;
using Books.Console;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


var startup = new Startup();
var services = new ServiceCollection();

startup.ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var serviceManager = serviceProvider.GetRequiredService<IServiceManager>();

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

foreach (var book in filteredBooks)
{
    Console.WriteLine($"{book.Title}");
}

async Task<List<Book>> SearchingBook(FilterConditions filterConditions)
{
    var filteredBooks = serviceManager.BookService.GetFilteredBooksAsync(filterConditions);
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

            Log.Logger = new LoggerConfiguration().WriteTo.File($"logs/{errorFileName}").CreateLogger();

            if (DataCheck(fields))
            {
                try
                {
                    await serviceManager.BookService.CreateBookAsync(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                    Log.Information(line);
                }
            }
            else
            {
                Log.Information(line);
            }
        }
    }
    Log.CloseAndFlush();

    Console.WriteLine();
    Console.WriteLine("Press something to continue..." + "\n");
    Console.ReadKey();
}

bool DataCheck(string[] fields)
{
    var title = fields[0];
    var genre = fields[2];
    var author = fields[4];
    var publisher = fields[5];

    if (fields[1].Equals("Pages", StringComparison.OrdinalIgnoreCase))
    {
        return false;
    }

    return title is string && genre is string && author is string && publisher is string;
}
