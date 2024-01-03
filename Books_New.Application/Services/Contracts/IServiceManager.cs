using Books_New.Application.Services.Contracts.Services;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAuthorService AuthorService { get; }   
        IBookService BookService { get; }
        IGenreService GenreService { get; }
        IPublisherService PublisherService { get; }
    }
}
