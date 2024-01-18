namespace Books_New.Application
{ 
    public interface IServiceManager
    {
        IAuthorService AuthorService { get; }   
        IBookService BookService { get; }
        IGenreService GenreService { get; }
        IPublisherService PublisherService { get; }
    }
}
