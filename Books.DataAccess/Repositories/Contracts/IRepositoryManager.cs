namespace Books.DataAccess
{
    public interface IRepositoryManager
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IGenreRepository Genre { get; }
        IPublisherRepository Publisher { get; }
        void Save();
    }
}
