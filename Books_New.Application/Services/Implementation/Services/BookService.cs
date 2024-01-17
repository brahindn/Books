using Books_New.Application.Services.Contracts.Services;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Service
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }

        public void CreateBook(string title, string pages, string genreName, string releaseDate, string authorName, string publisherName)
        {
            var existBook = _repositoryManager.Book.CheckDuplicate(title, genreName, authorName, publisherName);

            if (!existBook)
            {
                return;
            }

            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(authorName) || string.IsNullOrEmpty(genreName) || string.IsNullOrEmpty(publisherName))
            {
                return;
            }

            var author = _repositoryManager.Author.GetAuthor(authorName) ?? new Author {Name = authorName};
            var genre = _repositoryManager.Genre.GetGenre(genreName) ?? new Genre { Name = genreName };
            var publisher = _repositoryManager.Publisher.GetPublisher(publisherName) ?? new Publisher {  Name = publisherName };
            int page;
            DateTime time;

            var book = new Book
            {
                Title = title,
                Pages = int.TryParse(pages, out page) ? page : null,
                Genre = genre,
                Author = author,
                Publisher = publisher,
                ReleaseDate = DateTime.TryParse(releaseDate, out time) ? time : null
            };

            _repositoryManager.Book.CreateBook(book);
            _repositoryManager.Save();
        }

        public IQueryable<Book> GetBook(string name)
        {
            var filterBook = _repositoryManager.Book.GetBook(name);

            return filterBook;
        }
    }
}
