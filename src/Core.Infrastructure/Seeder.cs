using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;
using Library.Mongo;
using MongoDB.Driver;

namespace Core.Infrastructure
{
    public abstract class Seeder<T> : ISeeder where T : class, IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public Seeder(IMongoContext mongoContext)
        {
            _collection = mongoContext.GetCollection<T>();
        }

        public abstract Task Seed();

        protected async Task Store(T item, Expression<Func<T, bool>> expression)
        {
            try
            {
                if (_collection.AsQueryable().Any())
                {
                    var storedItem = await _collection.Find(expression).FirstOrDefaultAsync();
                    if (storedItem != null)
                    {
                        item.Id = storedItem.Id;
                        await _collection.ReplaceOneAsync(x => x.Id == item.Id, item);
                        return;
                    }
                }
                
                item.Id = Guid.NewGuid().ToString();
                await _collection.InsertOneAsync(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
