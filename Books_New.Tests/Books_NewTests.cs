using Books_New.DataAccess.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;

namespace Books_New.Tests
{
    [TestClass]
    public class Books_NewTests
    {
        private readonly ServiceManager _serviceManager;
        private readonly DbContextOptions<RepositoryContext> _options;

        public Books_NewTests()
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
            _serviceManager.AuthorService.CreateAuthor("Spange Bob");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Authors.Count());
                Assert.AreEqual("Spange Bob", context.Authors.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullAuthorToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthor(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddEmptyAuthorToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthor(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddNewGenreToDataBase()
        {
            _serviceManager.GenreService.CreateGenre("Horror");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Genres.Count());
                Assert.AreEqual("Horror", context.Genres.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullGenreToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthor(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddEmptyGenreToDataBase()
        {
            _serviceManager.AuthorService.CreateAuthor(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Authors.Count());
            }
        }

        [TestMethod]
        public void AddNewPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisher("City Classic");

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(1, context.Publishers.Count());
                Assert.AreEqual("City Classic", context.Publishers.Single().Name);
            }
        }

        [TestMethod]
        public void AddNullPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisher(null);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public void AddEmptyPublisherToDataBase()
        {
            _serviceManager.PublisherService.CreatePublisher(string.Empty);

            using (var context = new RepositoryContext(_options))
            {
                Assert.AreEqual(0, context.Publishers.Count());
            }
        }

        [TestMethod]
        public void AddNewBookToDataBase_ItIsMethodUpdate()
        {
            _serviceManager.BookService.CreateBook("Title", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");

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
    }
}