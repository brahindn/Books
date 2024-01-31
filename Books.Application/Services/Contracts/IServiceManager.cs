using Books.Application.Services.Contracts.Services;

namespace Books.Application.Services.Contracts
{
    public interface IServiceManager
    {
        IAuthorService AuthorService { get; }
        IBookService BookService { get; }
        IGenreService GenreService { get; }
        IPublisherService PublisherService { get; }
    }
}
