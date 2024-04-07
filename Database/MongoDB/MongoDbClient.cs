using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GLogger.Database.MongoDB
{
    public class MongoDbClient
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbClient(IOptions<DatabaseConfig> config)
        {
            _client = new MongoClient(config.Value.ConnectionString);
            _database = _client.GetDatabase(config.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public void DropCollection(string collectionName)
        {
            _database.DropCollection(collectionName);
        }
    }
}