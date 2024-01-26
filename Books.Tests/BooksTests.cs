using Books.Application;
using Books.DataAccess;
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

            var context = new RepositoryContext(_options);
            var repositoryManager = new RepositoryManager(context);

            _serviceManager = new ServiceManager(repositoryManager);

             context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddNewAuthorToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthorAsync("Spange Bob");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Authors.Count());
                Assert.AreEqual("Spange Bob", context.Authors.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullAuthorToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthorAsync(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddEmptyAuthorToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthorAsync(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddNewGenreToDataBase()
        {
            _serviceManager.GenreService.CreateGenreAsync("Horror");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Genres.Count());
                Assert.AreEqual("Horror", context.Genres.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullGenreToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthorAsync(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddEmptyGenreToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthorAsync(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddNewPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisherAsync("City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Publishers.Count());
                Assert.AreEqual("City Classic", context.Publishers.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisherAsync(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public void AddEmptyPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisherAsync(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase()
        {
            _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext(_options))
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
        public void AddNewBookToDataBase_WithoutTitle_0Books()
        {
            _serviceManager.BookService.CreateBookAsync("", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_WithoutPages()
        {
            _serviceManager.BookService.CreateBookAsync("Title", "", "Horror", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_WithoutGenre_0Books()
        {
            _serviceManager.BookService.CreateBookAsync("Title", "500", "", "2017-12-10", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_WithoutRealiseDate()
        {
            _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "", "Spange Bob", "City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Books.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_WithoutAuthor_0Books()
        {
            _serviceManager.BookService.CreateBookAsync("Title", "500", "Horror", "2017-12-10", "", "City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_WithoutPublisher_0Books()
        {
            _serviceManager.BookService.CreateBookAsync("", "500", "Horror", "2017-12-10", "Spange Bob", "");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Books.Count());
            }
        }
    }
}