using Entities.Models;

namespace Contracts
{
    public interface IBookRepository
    {
        void CreateBook(Book book);
    }
}
