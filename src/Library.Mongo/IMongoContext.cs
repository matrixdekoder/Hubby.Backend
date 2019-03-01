using MongoDB.Driver;

namespace Library.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}
