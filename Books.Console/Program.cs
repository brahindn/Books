﻿using Books.Application;
using Books.Console;
using Books.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;


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
    ReleaseDate = startup.FilterConditions.ReleaseDate
};

var filteredList = SearchingBook(filterConditions);

foreach(var book in filteredList)
{
    Console.WriteLine($"{book.Title} - {book.Author.Name}");
}

List<Book> SearchingBook(FilterConditions filterConditions)
{
    var result = new List<Book>();

    if (filterConditions != null)
    {
        if(filterConditions.BookName != null)
        {
            foreach (var bookName in filterConditions.BookName)
            {
                result.AddRange(serviceManager.BookService.GetBook(bookName));
            }
        }
        if(filterConditions.AuthorName != null)
        {
            foreach (var authorName in filterConditions.AuthorName)
            {
                result.AddRange(serviceManager.BookService.GetBook(authorName));
            }
        }
        if(filterConditions.GenreName != null)
        {
            foreach (var genreName in filterConditions.GenreName)
            {
                result.AddRange(serviceManager.BookService.GetBook(genreName));
            }
        }
        if(filterConditions.PublisherName != null)
        {
            foreach (var  publisherName in filterConditions.PublisherName)
            {
                result.AddRange(serviceManager.BookService.GetBook(publisherName));
            }
        }
        if (filterConditions.PageNumber != null)
        {
            foreach (var pageNumber in filterConditions.PageNumber)
            {
                if(int.TryParse(pageNumber, out int page))
                {
                    result.AddRange(serviceManager.BookService.GetBook(pageNumber));
                }
            }
        }
        if (filterConditions.ReleaseDate != null)
        {
            foreach (var releaseDate in filterConditions.ReleaseDate)
            {
                if (DateTime.TryParse(releaseDate, out DateTime date))
                {
                    result.AddRange(serviceManager.BookService.GetBook(releaseDate));
                }
            }
        }
    }

    return result.GroupBy(b => new { b.Title, b.Author.Name }).Select(b => b.First()).ToList();
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
            string errorFileName = $"{DateTime.Now:yyyyMMdd_HHmm}.txt";
            string errorFilePath = Path.Combine(Directory.GetCurrentDirectory(), errorFileName);

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
    List<string> dateFormats = new List<string>();

    foreach(var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
        dateFormats.Add(culture.DateTimeFormat.ShortDatePattern);
    }

    return fields[0] is string && int.TryParse(fields[1], out int page) && fields[2] is string && DateTime.TryParseExact(fields[3], dateFormats.ToArray(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time) && fields[4] is string && fields[5] is string;
}
