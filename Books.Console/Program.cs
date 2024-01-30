using Books.Application;
using Books.Console;
using Books.Domain;
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

var filteredList = await SearchingBook(filterConditions);

foreach(var book in filteredList)
{
    Console.WriteLine($"{book.Title} - {book.Author.Name}");
}

async Task<List<Book>> SearchingBook(FilterConditions filterConditions)
{
    var result = new List<Book>();

    if (filterConditions != null)
    {
        var books = await serviceManager.BookService.GetBooksAsync(filterConditions);
        result.AddRange(books);
    }

    return result.GroupBy(b => new { b.Title, b.Author.Name }).Select(b => b.First()).ToList();
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
                catch
                {
                    Console.WriteLine("(Something went wrong)");
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
    return fields[0] is string && int.TryParse(fields[1], out int page) && fields[2] is string && fields[4] is string && fields[5] is string;
}
