using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
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
