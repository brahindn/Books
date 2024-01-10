using Entities.Models;

namespace Books_New.Application.Services.Contracts.Services
{
    public interface IPublisherService
    {
        void CreatePublisher(string field);
        Publisher GetPublisher(string name);
    }
}
