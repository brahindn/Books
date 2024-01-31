using Books.DataAccess.Repositories.Contracts.Repositories;

namespace Books.DataAccess.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IGenreRepository Genre { get; }
        IPublisherRepository Publisher { get; }
        Task SaveAsync();
    }
}
