using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Factories;
using MedicalServicesManagement.DAL.Interfaces;
using MongoDB.Driver;

namespace MedicalServicesManagement.DAL.Repositories
{
    public abstract class MongoRepository<TMongoEntity> : IMongoRepository<TMongoEntity>
        where TMongoEntity : BaseMongoEntity
    {
        public abstract string CollectionName { get; }

        protected readonly IMongoCollection<TMongoEntity> _collection;

        protected MongoRepository(IMongoDbFactory mongoDbFactory)
        {
            _collection = mongoDbFactory.GetCollection<TMongoEntity>(CollectionName);
        }

        public async Task<List<TMongoEntity>> GetAllAsync()
        {
            var items = await _collection.FindAsync(_ => true);

            return await items.ToListAsync();
        }

        public async Task<TMongoEntity> GetByIdAsync(string id)
        {
            var item = await _collection.FindAsync(Builders<TMongoEntity>.Filter.Eq("_id", id));

            return await item.FirstOrDefaultAsync();
        }

        public async Task<List<TMongoEntity>> FilterAsync(Expression<Func<TMongoEntity, bool>> expression)
        {
            var items = await _collection.FindAsync(Builders<TMongoEntity>.Filter.Where(expression));

            return await items.ToListAsync();
        }

        public async Task<List<TMongoEntity>> FilterAsync<TField>(Expression<Func<TMongoEntity, TField>> field, IEnumerable<TField> values)
        {
            var items = await _collection.FindAsync(Builders<TMongoEntity>.Filter.In(field, values));

            return await items.ToListAsync();
        }

        public Task CreateAsync(TMongoEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            return _collection.InsertOneAsync(entity);
        }

        public Task UpdateAsync(string id, TMongoEntity entity)
        {
            return _collection.ReplaceOneAsync(Builders<TMongoEntity>.Filter.Eq("_id", id), entity);
        }

        public Task UpdateAsync<TField>(string id, Expression<Func<TMongoEntity, TField>> field, TField newValue)
        {
            return _collection.FindOneAndUpdateAsync(Builders<TMongoEntity>.Filter.Eq("_id", id),
                    Builders<TMongoEntity>.Update.Set(field, newValue));
        }

        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(Builders<TMongoEntity>.Filter.Eq("_id", id));
        }
    }
}
