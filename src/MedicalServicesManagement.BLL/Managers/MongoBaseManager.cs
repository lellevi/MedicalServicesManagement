using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public class MongoBaseManager<TEntity, TDto> : IMongoBaseManager<TEntity, TDto>
    where TEntity : BaseMongoEntity
    where TDto : IMongoDto
    {
        protected readonly IMongoRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public MongoBaseManager(IMongoRepository<TEntity> mongorepository, IMapper mapper)
        {
            _repository = mongorepository;
            _mapper = mapper;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<TDto>> GetAllDtosAsync()
        {
            var entities = await GetAllAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.CreateAsync(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(string id, TEntity entity)
        {
            await _repository.UpdateAsync(id, entity);
        }

        public async Task UpdateAsync<TField>(string id, Expression<Func<TEntity, TField>> field, TField newValue)
        {
            await _repository.UpdateAsync(id, field, newValue);
        }

        public async Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.FilterAsync(expression);
        }

        public async Task<List<TEntity>> FilterAsync<TField>(Expression<Func<TEntity, TField>> field, IEnumerable<TField> values)
        {
            return await _repository.FilterAsync(field, values);
        }

        public Task CreateAsync(TEntity entity)
        {
            return _repository.CreateAsync(entity);
        }
    }
}
