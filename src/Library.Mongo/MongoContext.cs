using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Library.Mongo
{
    public class MongoContext: IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoConfiguration> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
