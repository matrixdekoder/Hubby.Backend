using System;
using System.Threading.Tasks;
using Core.Application;
using MongoDB.Driver;

namespace Library.Mongo
{
    public class MongoProjectionWriter : IProjectionWriter
    {
        private readonly IMongoContext _context;

        public MongoProjectionWriter(IMongoContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T view) where T : IReadModel
        {
            var collection = _context.GetCollection<T>();
            await collection.InsertOneAsync(view);
        }

        public async Task Update<T>(string id, Action<T> updateActions) where T : IReadModel
        {
            var collection = _context.GetCollection<T>();
            var view = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (view != null)
            {
                updateActions(view);
                await collection.ReplaceOneAsync(x => x.Id == view.Id, view);
            }
        }
    }
}
