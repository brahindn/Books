using Books.Application.Services.Contracts.Services;
using Books.DataAccess.Repositories.Contracts;
using Books.Domain.Entities;
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

        public async Task<IQueryable<Book>> GetFilteredBooksAsync(FilterConditions filterConditions)
        {
            var query = _repositoryManager.Book.GetAllBooks();

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
                    query = query.Where(b => pageNumber.Contains(b.Pages.Value));
                }
                if(filterConditions.ReleaseDate != null && filterConditions.ReleaseDate.Any())
                {
                    var date = filterConditions.ReleaseDate.Select(DateTime.Parse).ToList();
                    query = query.Where(b => date.Contains(b.ReleaseDate.Value));
                }
            }
            
            return await _repositoryManager.Book.GetFilteredBooksAsync(query);
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
