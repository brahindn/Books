using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PublisherService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public PublisherService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repositoryManager = repository;
            _logger = logger;
        }
    }
}
