using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public AuthorService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repositoryManager = repository;
            _logger = logger;
        }
    }
}
