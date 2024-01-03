using Books_New.Application.Services.Contracts.Services;
using Contracts;
using Service.Contracts;
namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IGenreService> _genreService;
        private readonly Lazy<IPublisherService> _publisherService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager));
            _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager));
            _genreService = new Lazy<IGenreService>(() => new GenreService(repositoryManager));
            _publisherService = new Lazy<IPublisherService>(() => new PublisherService(repositoryManager));
        }

        public IAuthorService AuthorService => _authorService.Value;
        public IBookService BookService => _bookService.Value;
        public IGenreService GenreService => _genreService.Value;
        public IPublisherService PublisherService => _publisherService.Value;
    }
}
