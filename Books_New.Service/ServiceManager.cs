using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
