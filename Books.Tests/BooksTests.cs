using Books.Application.Services.Implementation;
using Books.DataAccess;
using Books.DataAccess.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Books.Tests
{
    [TestClass]
    public class BooksTests
    {
        private readonly ServiceManager _serviceManager;
        private readonly DbContextOptions<RepositoryContext> _options;

        public BooksTests() 
        {
           _options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new RepositoryContext();
            var repositoryManager = new RepositoryManager(context);

            _serviceManager = new ServiceManager(repositoryManager);

             context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task AddNewAuthorToDataBase()
        {
            await _serviceManager.AuthorService.CreateAuthorAsync("Spange Bob");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Authors.Count());
                Assert.AreEqual("Spange Bob", context.Authors.Single().Name);
            }
        }

        [TestMethod]
        public async Task AddNullAuthorToDataBase()
        {
            await _serviceManager.AuthorService.CreateAuthorAsync(null);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public async Task AddEmptyAuthorToDataBase()
        {
            await _serviceManager.AuthorService.CreateAuthorAsync(string.Empty);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public async Task AddNewGenreToDataBase()
        {
            await _serviceManager.GenreService.CreateGenreAsync("Horror");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Genres.Count());
                Assert.AreEqual("Horror", context.Genres.Single().Name);
            }
        }

        [TestMethod]
        public async Task AddNullGenreToDataBase()
        {
            await _serviceManager.AuthorService.CreateAuthorAsync(null);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public async Task AddEmptyGenreToDataBase()
        {
            await _serviceManager.AuthorService.CreateAuthorAsync(string.Empty);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public async Task AddNewPublisherToDataBase()
        {
            await _serviceManager.PublisherService.CreatePublisherAsync("City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Publishers.Count());
                Assert.AreEqual("City Classic", context.Publishers.Single().Name);
            }
        }

        [TestMethod]
        public async Task AddNullPublisherToDataBase()
        {
            await _serviceManager.PublisherService.CreatePublisherAsync(null);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public async Task AddEmptyPublisherToDataBase()
        {
            await _serviceManager.PublisherService.CreatePublisherAsync(string.Empty);

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase()
        {
            await _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Books.Count());
                Assert.AreEqual(1, context.Authors.Count());
                Assert.AreEqual(1, context.Genres.Count());
                Assert.AreEqual(1, context.Publishers.Count());

                Assert.AreEqual("Title", context.Books.Single().Title);
                Assert.AreEqual("Horror", context.Genres.Single().Name);
                Assert.AreEqual("Spange Bob", context.Authors.Single().Name);
                Assert.AreEqual("City Classic", context.Publishers.Single().Name);
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutTitle_0Books()
        {
            try
            {
                await _serviceManager.BookService.CreateBookAsync("", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");
            }
            catch
            {

            }

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutPages()
        {
            await _serviceManager.BookService.CreateBookAsync("Title", "", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WherePagesIsABC()
        {
            await _serviceManager.BookService.CreateBookAsync("Title", "ABC", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WherePagesIsSymbols()
        {
            await _serviceManager.BookService.CreateBookAsync("Title", "!@$", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutGenre_0Books()
        {
            try
            {
                await _serviceManager.BookService.CreateBookAsync("Title", "500", "", "2017-12-10", "Spange Bob", "City Classic");
            }
            catch
            {

            }

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutRealiseDate()
        {
            await _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutAuthor_0Books()
        {
            try
            {
                await _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "2017-12-10", "", "City Classic");
            }
            catch(ArgumentException)
            {

            }

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public async Task AddNewBookToDataBase_WithoutPublisher_0Books()
        {
            try
            {
                await _serviceManager.BookService.CreateBookAsync("", "500", "Horror", "2017-12-10", "Spange Bob", "");
            }
            catch
            {

            }

            using (var context = new RepositoryContext())
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }
    }
}