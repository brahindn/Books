using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;

        public PublisherService(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }
    }
}
