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
    public class MedSpecialityService : IMedSpecialityService
    {
        private readonly IRepository<MedSpeciality> _repository;
        private readonly IMapper _mapper;

        public MedSpecialityService(IRepository<MedSpeciality> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private void Validate(MedSpecialityDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (item.Name.Length > 50)
                throw new ArgumentException("Name max length is 50");
        }

        public async Task CreateAsync(MedSpecialityDTO item)
        {
            try
            {
                Validate(item);
                var entity = _mapper.Map<MedSpeciality>(item);
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating medSpeciality: " + ex.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("MedSpeciality not found.");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<MedSpecialityDTO>> GetAllAsync()
        {
            var entities = await (await _repository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<MedSpecialityDTO>>(entities);
        }

        public async Task<MedSpecialityDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<MedSpecialityDTO>(entity);
        }

        public async Task UpdateAsync(MedSpecialityDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Validate(item);

            var existing = await _repository.GetByIdAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"MedSpeciality not found.");

            var entity = _mapper.Map<MedSpeciality>(item);
            await _repository.UpdateAsync(entity);
        }
    }
}
