using MongoDB.Driver;

namespace Library.Mongo.Persistence
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}
