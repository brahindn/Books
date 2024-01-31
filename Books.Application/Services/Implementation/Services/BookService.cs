using Books.Application.Services.Contracts.Services;
using Books.DataAccess.Repositories.Contracts;
using Books.Domain;
using Books.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace Books.Application.Services.Implementation.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public async Task CreateBookAsync(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(authorName) || string.IsNullOrEmpty(genreName) || string.IsNullOrEmpty(publisherName))
            {
                throw new ArgumentException("Title, AuthorName, GenreName or PublisherName cannot be null or empty.");
            }   

            var existBook = _repositoryManager.Book.CheckDuplicate(title, genreName, authorName, publisherName);

            if (!existBook)
            {
                return;
            }

            var author = await _repositoryManager.Author.GetAuthorAsync(authorName) ?? new Author { Name = authorName };
            var genre = await _repositoryManager.Genre.GetGenreAsync(genreName) ?? new Genre { Name = genreName };
            var publisher = await _repositoryManager.Publisher.GetPublisherAsync(publisherName) ?? new Publisher { Name = publisherName };

            var book = new Book
            {
                Title = title,
                Pages = int.TryParse(pages, out int page) ? page : null,
                Genre = genre,
                Author = author,
                Publisher = publisher,
                ReleaseDate = DateTime.TryParse(DateConverter(releaseDate), out DateTime date) ? date : null
            };

            _repositoryManager.Book.Create(book);
            await _repositoryManager.SaveAsync();
        }

        public async Task<List<Book>> GetBooksAsync(FilterConditions filterConditions)
        {
            var result = new List<Book>();

            var allBooks = await _repositoryManager.Book.GetAllBooksAsync();




            if (filterConditions != null)
            {
                if (filterConditions.BookName != null)
                {
                    foreach (var bookName in filterConditions.BookName)
                    {
                        var books = allBooks.Where(b => b.Title == bookName).ToList();
                        result.AddRange(books);
                    }
                }
                if (filterConditions.AuthorName != null)
                {
                    foreach (var authorName in filterConditions.AuthorName)
                    {
                        var books = allBooks.Where(b => b.Author.Name == authorName).ToList();
                        result.AddRange(books);
                    }
                }
                if (filterConditions.GenreName != null)
                {
                    foreach (var genreName in filterConditions.GenreName)
                    {
                        var books = allBooks.Where(b => b.Genre.Name == genreName).ToList();
                        result.AddRange(books);
                    }
                }
                if (filterConditions.PublisherName != null)
                {
                    foreach (var publisherName in filterConditions.GenreName)
                    {
                        var books = allBooks.Where(b => b.Title == publisherName).ToList();
                        result.AddRange(books);
                    }
                }
                if (filterConditions.PageNumber != null)
                {
                    foreach (var pageNumber in filterConditions.PageNumber)
                    {
                        int.TryParse(pageNumber, out int page);
                        var books = allBooks.Where(b => b.Pages.Value == page).ToList();
                        result.AddRange(books);
                    }
                }
                if (filterConditions.ReleaseDate != null)
                {
                    foreach (var releaseDate in filterConditions.ReleaseDate)
                    {
                        DateTime.TryParse(releaseDate, out DateTime date);
                        var books = allBooks.Where(b => b.ReleaseDate.Value == date).ToList();
                        result.AddRange(books);
                    }
                }
            }

            return result;
        }





        private static string DateConverter(string releaseDate)
        {
            List<string> dateFormats = new List<string>();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                dateFormats.Add(culture.DateTimeFormat.ShortDatePattern);
            }

            try
            {
                var date = DateTime.ParseExact(releaseDate, dateFormats.ToArray(), CultureInfo.InvariantCulture);
                var ukraineCulture = new CultureInfo("uk-UK");

                return date.ToString(ukraineCulture.DateTimeFormat);
            }
            catch
            {
                var date = "";
                return date;
            }
        }
    }
}
