using Books.Application.Services.Contracts.Services;
using Books.DataAccess.Repositories.Contracts;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
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
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(genreName) || string.IsNullOrWhiteSpace(publisherName))
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
                ReleaseDate = DateConverter(releaseDate) ?? null
            };

            _repositoryManager.Book.Create(book);
            await _repositoryManager.SaveAsync();
        }

        public IQueryable<Book> GetFilteredBooksAsync(FilterConditions filterConditions)
        {
            var query = _repositoryManager.Book.FindAll();

            if(filterConditions != null)
            {
                if(filterConditions.BookName != null && filterConditions.BookName.Any())
                {
                    query = query.Where(b => filterConditions.BookName.Contains(b.Title));
                }
                if(filterConditions.AuthorName != null && filterConditions.AuthorName.Any())
                {
                    query = query.Where(b => filterConditions.AuthorName.Contains(b.Author.Name));
                }
                if(filterConditions.GenreName != null && filterConditions.GenreName.Any())
                {
                    query = query.Where(b => filterConditions.GenreName.Contains(b.Genre.Name));
                }
                if(filterConditions.PublisherName != null && filterConditions.PublisherName.Any())
                {
                    query = query.Where(b => filterConditions.PublisherName.Contains(b.Publisher.Name));
                }
                if(filterConditions.PageNumber != null && filterConditions.PageNumber.Any())
                {
                    var pageNumber = filterConditions.PageNumber.Select(int.Parse).ToList();
                    
                    foreach(var number in pageNumber)
                    {
                        query = query.Where(b => b.Pages.Value == number);
                    }
                }
                if(filterConditions.ReleaseDate != null && filterConditions.ReleaseDate.Any())
                {
                    var releaseDate = filterConditions.ReleaseDate.Select(DateTime.Parse).ToList();

                    foreach (var date in releaseDate)
                    {
                        query = query.Where(b => b.ReleaseDate.Value == date);
                    }
                }
            }
            
            return _repositoryManager.Book.GetFilteredBooksAsync(query);
        }

        private static DateTime? DateConverter(string releaseDate)
        {
            List<string> dateFormats = new List<string>();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                dateFormats.Add(culture.DateTimeFormat.ShortDatePattern);
            }

            try
            {
                var ukraineCulture = new CultureInfo("uk-UK");
                var date = DateTime.ParseExact(releaseDate, dateFormats.ToArray(), ukraineCulture);

                return date;
            }
            catch
            {
                return null;
            }
        }
    }
}
