using Books.DataAccess;
using Books.Domain;
using System.Globalization;

namespace Books.Application
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
            var existBook = _repositoryManager.Book.CheckDuplicate(title, genreName, authorName, publisherName);

            if (!existBook)
            {
                return;
            }

            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(authorName) || string.IsNullOrEmpty(genreName) || string.IsNullOrEmpty(publisherName))
            {
                return;
            }

            var author = await _repositoryManager.Author.GetAuthorAsync(authorName) ?? new Author {Name = authorName};
            var genre = await _repositoryManager.Genre.GetGenreAsync(genreName) ?? new Genre { Name = genreName };
            var publisher = await _repositoryManager.Publisher.GetPublisherAsync(publisherName) ?? new Publisher {  Name = publisherName };

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

        public async Task<IQueryable<Book>> GetBookAsync(string name)
        {
            return await _repositoryManager.Book.GetBookAsync(name);
        }

        public async Task<IQueryable<Book>> GetBookAsync(int pages)
        {
            return await _repositoryManager.Book.GetBookAsync(pages);
        }

        public async Task<IQueryable<Book>> GetBookAsync(DateTime realiseDate)
        {
            return await _repositoryManager.Book.GetBookAsync(realiseDate);
        }

        private string DateConverter(string releaseDate)
        {
            List<string> dateFormats = new List<string>();
            
            foreach(var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                dateFormats.Add(culture.DateTimeFormat.ShortDatePattern);
            }

            var date = DateTime.ParseExact(releaseDate, dateFormats.ToArray(), CultureInfo.InvariantCulture);
            var ukraineCulture = new CultureInfo("uk-UK");

            return date.ToString(ukraineCulture.DateTimeFormat);
        }
    }
}
