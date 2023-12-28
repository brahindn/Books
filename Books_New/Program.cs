using Books_New.ContextFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;

var context = new RepositoryContextFactory();

context.CreateDbContext(args);


