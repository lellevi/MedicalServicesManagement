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
    public class AppointmentServiceService : IAppointmentServiceService
    {
        private readonly IRepository<AppointmentServiceDAL> _repository;
        private readonly IMapper _mapper;

        public AppointmentServiceService(IRepository<AppointmentServiceDAL> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private void Validate(AppointmentServiceDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.AdditionalServiceId) || item.AdditionalServiceId.Length > 36)
                throw new ArgumentException("AdditionalServiceId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.AppointmentId) || item.AppointmentId.Length > 36)
                throw new ArgumentException("AppointmentId is required and max length 36");
            if (item.Amount < 1)
                throw new ArgumentException("Amount must be at least 1");
        }

        public async Task CreateAsync(AppointmentServiceDTO item)
        {
            try
            {
                Validate(item);
                var entity = _mapper.Map<AppointmentServiceDAL>(item);
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating AppointmentService: " + ex.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("AppointmentService not found.");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<AppointmentServiceDTO>> GetAllAsync()
        {
            var entities = await (await _repository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<AppointmentServiceDTO>>(entities);
        }

        public async Task<AppointmentServiceDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<AppointmentServiceDTO>(entity);
        }

        public async Task UpdateAsync(AppointmentServiceDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Validate(item);

            var existing = await _repository.GetByIdAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"AppointmentService not found.");

            var entity = _mapper.Map<AppointmentServiceDAL>(item);
            await _repository.UpdateAsync(entity);
        }
    }
}
