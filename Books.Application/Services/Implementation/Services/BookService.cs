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

        public void CreateBook(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName)
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

            var author = _repositoryManager.Author.GetAuthor(authorName) ?? new Author { Name = authorName };
            var genre = _repositoryManager.Genre.GetGenre(genreName) ?? new Genre { Name = genreName };
            var publisher = _repositoryManager.Publisher.GetPublisher(publisherName) ?? new Publisher {  Name = publisherName };

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
            _repositoryManager.Save();
        }

        public IQueryable<Book> GetBook(string name)
        {
            return _repositoryManager.Book.GetBook(name);
        }

        public IQueryable<Book> GetBook(int pages)
        {
            return _repositoryManager.Book.GetBook(pages);
        }

        public IQueryable<Book> GetBook(DateTime realiseDate)
        {
            return _repositoryManager.Book.GetBook(realiseDate);
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
