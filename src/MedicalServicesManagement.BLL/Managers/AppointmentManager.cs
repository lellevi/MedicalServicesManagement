using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public AppointmentManager(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private void Validate(AppointmentDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.PatientId) || item.PatientId.Length > 36)
                throw new ArgumentException("PatientId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.ServiceId) || item.ServiceId.Length > 36)
                throw new ArgumentException("ServiceId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.MedicId) || item.MedicId.Length > 36)
                throw new ArgumentException("MedicId is required and max length 36");
            if (item.StartDate >= item.EndDate)
                throw new ArgumentException("StartDate must be before EndDate");
            if (item.TotalCost < 0)
                throw new ArgumentException("TotalCost must be zero or positive");
        }

        public async Task CreateAsync(AppointmentDTO item)
        {
            try
            {
                Validate(item);
                var entity = _mapper.Map<Appointment>(item);
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating appointment: " + ex.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Appointment not found.");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<AppointmentDTO>> GetAllAsync()
        {
            var entities = await (await _repository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<AppointmentDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<AppointmentDTO>(entity);
        }

        public async Task UpdateAsync(AppointmentDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Validate(item);

            var existing = await _repository.GetByIdAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Appointment not found.");

            var entity = _mapper.Map<Appointment>(item);
            await _repository.UpdateAsync(entity);
        }
    }
}
