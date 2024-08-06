using MongoDB.Driver;
using sciences_nation_back.Models;

namespace sciences_nation_back.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            CreateIndexes();
        }

        private void CreateIndexes()
        {
            var userCollection = _database.GetCollection<User>("Users");
            var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(user => user.Email);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<User>(indexKeysDefinition, indexOptions);
            userCollection.Indexes.CreateOne(indexModel);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
