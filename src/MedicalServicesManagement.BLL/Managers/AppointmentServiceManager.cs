using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class AppointmentServiceManager : BaseManager<AppointmentServiceDTO, AppointmentService>, IAppointmentServiceManager
    {
        public AppointmentServiceManager(ISqlRepository<AppointmentService> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override string EntityName { get => "appointmentService"; }

        public async Task<List<AppointmentServiceDTO>> GetAllIncludingSpecialitiesAsync()
        {
            var entities = await _repository.GetAllAsync(includes: [x => x.AdditionalService, x => x.AdditionalService.MedSpeciality]);
            return _mapper.Map<List<AppointmentServiceDTO>>(entities);
        }

        public async Task<List<AppointmentServiceDTO>> GetByAppointmentIdAsync(string appointmentId)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => x.AppointmentId == appointmentId,
                includes: [x => x.AdditionalService]);
            return _mapper.Map<List<AppointmentServiceDTO>>(entities);
        }

        protected override void Validate(AppointmentServiceDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (string.IsNullOrWhiteSpace(item.AdditionalServiceId) || item.AdditionalServiceId.Length > 36)
            {
                throw new ArgumentException("AdditionalServiceId is required and max length 36");
            }

            if (string.IsNullOrWhiteSpace(item.AppointmentId) || item.AppointmentId.Length > 36)
            {
                throw new ArgumentException("AppointmentId is required and max length 36");
            }

            if (item.Amount < 1)
            {
                throw new ArgumentException("Amount must be at least 1");
            }
        }
    }
}
