using Books_New.Application;
using Books_New.Console;
using Books_New.Entities;
using Microsoft.Extensions.DependencyInjection;


var startup = new Startup();
var services = new ServiceCollection();

startup.ConfigerationServices(services);

var serviceProvider = services.BuildServiceProvider();
var serviceManager = serviceProvider.GetService<IServiceManager>();

Console.WriteLine("Enter path to file");
string path = Console.ReadLine();

PopulateDatabaseFromFile(path);

var filterConditions = new FilterConditions()
{
    BookName = startup.FilterConditions.BookName,
    AuthorName = startup.FilterConditions.AuthorName,
    GenreName = startup.FilterConditions.GenreName,
    PublisherName = startup.FilterConditions.PublisherName,
    PageNumber = startup.FilterConditions.PageNumber,
    RealiseDate = startup.FilterConditions.RealiseDate
};

var filterBookList = SearchingBook(filterConditions.BookName);
ShowBooksWithFilter(filterBookList, "Data filtered by TITLE:");

var filterAuthorList = SearchingBook(filterConditions.AuthorName);
ShowBooksWithFilter(filterAuthorList, "Data filtered by AUTHOR:");

var filterGenreList = SearchingBook(filterConditions.GenreName);
ShowBooksWithFilter(filterGenreList, "Data filtered by GENRE:");

var filterPublisherList = SearchingBook(filterConditions.PublisherName);
ShowBooksWithFilter(filterPublisherList, "Data filtered by PUBLISHER:");

var filterPageList = SearchingBook(filterConditions.PageNumber);
ShowBooksWithFilter(filterPageList, "Data filtered by PAGES:");

var filterRealiseDateList = SearchingBook(filterConditions.RealiseDate);
ShowBooksWithFilter(filterRealiseDateList, "Data filtered by REALISE DATE:");


void ShowBooksWithFilter(List<Book> list, string message)
{
    Console.WriteLine(message);

    foreach (var entity in list)
    {
        Console.WriteLine($"{entity.Title} - {entity.Author.Name}");
    }

    Console.WriteLine();
    Console.WriteLine("Press something to continue..." + "\n");
    Console.ReadKey();
}

List<Book> SearchingBook(List<string> filterConditions)
{
    var result = new List<Book>();
    
    foreach (var bookName in filterConditions)
    {
        result.AddRange(serviceManager.BookService.GetBook(bookName));

        if(int.TryParse(bookName, out int page))
        {
            result.AddRange(serviceManager.BookService.GetBook(page));
        }

        if(DateTime.TryParse(bookName, out DateTime realiseDate))
        {
            result.AddRange(serviceManager.BookService.GetBook(realiseDate));
        }
    }

    return result;
}

void PopulateDatabaseFromFile(string path)
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
