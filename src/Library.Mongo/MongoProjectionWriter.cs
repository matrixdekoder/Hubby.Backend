using System;
using System.Threading.Tasks;
using Core.Application;
using Core.Domain;
using MongoDB.Driver;

namespace Library.Mongo
{
    public class MongoProjectionWriter<T>: IProjectionWriter<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoProjectionWriter(IMongoContext context)
        {
            _collection = context.GetCollection<T>();
        }

        public async Task Add(T view) 
        {
            await _collection.InsertOneAsync(view);
        }

        public async Task Update(string id, Action<T> updateActions)
        {
            var view = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (view != null)
            {
                updateActions(view);
                await _collection.ReplaceOneAsync(x => x.Id == view.Id, view);
            }
        }
    }
}
