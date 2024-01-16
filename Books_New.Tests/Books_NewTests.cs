using Books_New.DataAccess.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;

namespace Books_New.Tests
{
    [TestClass]
    public class Books_NewTests
    {
        private readonly BookService _bookService;
        private readonly DbContextOptions<RepositoryContext> _options;

        public Books_NewTests()
        {
           _options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new RepositoryContext(_options);
            var repositoryManager = new RepositoryManager(context);

            _bookService = new BookService(repositoryManager);
        }

        [TestMethod]
        public void AddNewBookToDataBase_ItIsMethodUpdate()
        {
            _bookService.CreateBook("Title", "500", "Horror", "2017-12-10", "Spange Bob", "City Classic");

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