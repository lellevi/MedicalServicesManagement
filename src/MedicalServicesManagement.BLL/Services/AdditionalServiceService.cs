using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Services
{
    public class AdditionalServiceService : IAdditionalServiceService
    {
        private readonly IRepository<AdditionalService> _repository;
        private readonly IMapper _mapper;

        public AdditionalServiceService(IRepository<AdditionalService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private void Validate(AdditionalServiceDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (item.Name.Length > 50)
                throw new ArgumentException("Name max length is 50");
            if (string.IsNullOrEmpty(item.MedSpecialityId) || item.MedSpecialityId.Length > 36)
                throw new ArgumentException("MedSpecialityId is required and max length 36");
            if (item.Price < 0)
                throw new ArgumentException("Price must be zero or positive");
        }

        public async Task CreateAsync(AdditionalServiceDTO item)
        {
            try
            {
                Validate(item);
                var entity = _mapper.Map<AdditionalService>(item);
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating additionalService: " + ex.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("AdditionalService not found.");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<AdditionalServiceDTO>> GetAllAsync()
        {
            var entities = await (await _repository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<AdditionalServiceDTO>>(entities);
        }

        public async Task<AdditionalServiceDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<AdditionalServiceDTO>(entity);
        }

        public async Task UpdateAsync(AdditionalServiceDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Validate(item);

            var existing = await _repository.GetByIdAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"AdditionalService not found.");

            var entity = _mapper.Map<AdditionalService>(item);
            await _repository.UpdateAsync(entity);
        }
    }
}
