using MongoDB.Driver;

namespace MedicalServicesManagement.DAL.Factories
{
    public class MongoDbFactory : IMongoDbFactory
    {
        private readonly string _databaseName;
        private readonly MongoClient _client;

        public MongoDbFactory(string connectionString, string databaseName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            _client = new MongoClient(settings);
            _databaseName = databaseName;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _client.GetDatabase(_databaseName).GetCollection<T>(collectionName);
        }
    }
}
