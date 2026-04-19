using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using MedicalServicesManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class AppointmentManager : BaseManager<AppointmentDTO, Appointment>, IAppointmentManager
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentManager(IAppointmentRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _appointmentRepository = repository;
        }

        protected override string EntityName { get => "appointment"; }

        public static void CheckStatus(Enums.AppointmentStatus existingStatus, Enums.AppointmentStatus newStatus)
        {
            var statusRules = new List<(Enums.AppointmentStatus newStatus, Enums.AppointmentStatus existingStatus, string message)>
            {
                (Enums.AppointmentStatus.Taken, Enums.AppointmentStatus.Free,
                 "Запись можно назначить только на свободный слот. Текущий статус: " + existingStatus),
                (Enums.AppointmentStatus.DoneNoPay, Enums.AppointmentStatus.Taken,
                 "Завершение без оплаты возможно только для назначенной записи. Текущий статус: " + existingStatus),
                (Enums.AppointmentStatus.DonePaid, Enums.AppointmentStatus.DoneNoPay,
                 "Оплата возможна только после завершения без оплаты. Текущий статус: " + existingStatus),
            };

            foreach (var (expectedNewStatus, expectedExistingStatus, message) in statusRules)
            {
                if (newStatus == expectedNewStatus && existingStatus != expectedExistingStatus)
                {
                    throw new ArgumentException(message, nameof(newStatus));
                }
            }
        }

        public async Task<List<AppointmentDTO>> GetAllFreeByMedicAndServiceOrderedAsync(string serviceId, string medicId = null)
        {
            var threeWeeksFromNow = DateTime.UtcNow.Date.AddDays(Constants.FreeAppointmentsPeriodDays);

            Expression<Func<Appointment, bool>> filter = medicId == null ?
                (x => x.Status == DAL.Entities.AppointmentStatus.Free && x.ServiceId == serviceId
                 && x.StartDate >= DateTime.UtcNow.Date && x.StartDate <= threeWeeksFromNow)
                : (x => x.Status == DAL.Entities.AppointmentStatus.Free
                && x.ServiceId == serviceId && x.MedicId == medicId
                 && x.StartDate >= DateTime.UtcNow.Date && x.StartDate <= threeWeeksFromNow);

            var entities = await _appointmentRepository.GetAllFreeAppointmentsOrderedAsync(filter, [x => x.Service, x => x.Medic]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public override async Task UpdateAsync(AppointmentDTO item)
        {
            try
            {
                Validate(item);

                var currentEntity = await GetByIdAsync(item.Id);

                CheckStatus(currentEntity.Status, item.Status);

                var entity = _mapper.Map<Appointment>(item);
                await _repository.UpdateAsync(entity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException($"Error updating {EntityName}: {ex.Message}", ex);
            }
        }

        public async Task<List<AppointmentDTO>> GetAllIncludingServiceAndMedicAsync()
        {
            var entities = await _repository.GetAllAsync(
                includes: [x => x.Service, x => x.Medic]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<List<AppointmentDTO>> GetAllAsync(string specialityId)
        {
            var entities = await _repository.GetAllAsync(filter: x => x.Service.MedSpecialityId == specialityId,
                includes: [x => x.Service]);
            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        protected override void Validate(AppointmentDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (string.IsNullOrWhiteSpace(item.PatientId) && item.Status != Enums.AppointmentStatus.Free)
            {
                throw new ArgumentException("Patient is required");
            }

            if (string.IsNullOrWhiteSpace(item.ServiceId))
            {
                throw new ArgumentException("Service is required");
            }

            if (string.IsNullOrWhiteSpace(item.MedicId))
            {
                throw new ArgumentException("Medic is required");
            }

            if (item.StartDate >= item.EndDate)
            {
                throw new ArgumentException("Start date must be before end date");
            }

            if (item.TotalCost < 0)
            {
                throw new ArgumentException("Total cost must be zero or positive");
            }
        }
    }
}
