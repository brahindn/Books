using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
