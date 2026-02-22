using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public abstract class BaseManager<TDTO, TEntity> : IManager<TDTO, TEntity>
        where TDTO : IDTO
        where TEntity : IEntity
    {
        protected abstract string EntityName { get; }

        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseManager(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected abstract void Validate(TDTO item);

        public async Task CreateAsync(TDTO item)
        {
            Validate(item);

            var entity = _mapper.Map<TEntity>(item);
            await _repository.CreateAsync(entity);
        }

        public async Task DeleteByIdAsync(string id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException($"Error deleting {EntityName}: {ex.Message}", ex);
            }
        }

        public async Task<List<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public async Task<TDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? default : _mapper.Map<TDTO>(entity);
        }

        public async Task UpdateAsync(TDTO item)
        {
            try
            {
                Validate(item);

                var entity = _mapper.Map<TEntity>(item);
                await _repository.UpdateAsync(entity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException($"Error updating {EntityName}: {ex.Message}", ex);
            }
        }

    }
}
