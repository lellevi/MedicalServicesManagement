using MongoDB.Driver;

namespace MedicalServicesManagement.DAL.Factories
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
