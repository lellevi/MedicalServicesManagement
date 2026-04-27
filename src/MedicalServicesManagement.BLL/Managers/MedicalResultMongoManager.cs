using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public class MedicalResultMongoManager : MongoBaseManager<MedicalResult, MedicalResultDto>, IMedicalResultMongoManager
    {
        public MedicalResultMongoManager(
            IMongoRepository<MedicalResult> repository,
            IMapper mapper)
            : base(repository, mapper)
        {
        }

        public async Task<MedicalResultDto> GetByAppointmentIdAsync(string appointmentId)
        {
            var entities = await _repository.FilterAsync(x => x.Appointment.Id == appointmentId);
            var entity = entities.FirstOrDefault();
            return entity == null ? null : _mapper.Map<MedicalResultDto>(entity);
        }

        public async Task<List<MedicalResultDto>> GetByPatientIdAsync(string patientId)
        {
            var entities = await _repository.FilterAsync(x => x.Patient.Id == patientId);
            return _mapper.Map<List<MedicalResultDto>>(entities);
        }

        public async Task CreateOrUpdateByAppointmentIdAsync(string appointmentId, MedicalResultDto dto)
        {
            if (dto.CreatedOn == default)
            {
                dto.CreatedOn = DateTime.UtcNow;
            }

            dto.ModifiedOn = DateTime.UtcNow;

            var existing = await GetByAppointmentIdAsync(appointmentId);
            if (existing != null)
            {
                dto.Id = existing.Id;

                var entity = _mapper.Map<MedicalResult>(dto);
                await UpdateAsync(entity.Id, entity);
            }
            else
            {
                await CreateAsync(dto);
            }
        }
    }
}
