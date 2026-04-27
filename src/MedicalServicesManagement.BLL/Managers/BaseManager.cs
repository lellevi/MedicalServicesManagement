using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.BLL.Managers
{
    internal abstract class BaseManager<TDTO, TEntity> : IManager<TDTO>
        where TDTO : IDTO
        where TEntity : class, IEntity
    {
        protected readonly ISqlRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected BaseManager(ISqlRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected abstract string EntityName { get; }

        public virtual async Task CreateAsync(TDTO item)
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
            var entity = await _repository.GetSingleAsync(x => x.Id == id);
            return entity == null ? default : _mapper.Map<TDTO>(entity);
        }

        public virtual async Task UpdateAsync(TDTO item)
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

        protected abstract void Validate(TDTO item);
    }
}
