using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class AppointmentManager : BaseManager<AppointmentDTO, Appointment>, IAppointmentManager
    {
        public AppointmentManager(IRepository<Appointment> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override string EntityName { get => "appointment"; }

        public static void CheckStatus(Enums.AppointmentStatus existingStatus, Enums.AppointmentStatus newStatus)
        {
            if (newStatus == Enums.AppointmentStatus.Taken && existingStatus != Enums.AppointmentStatus.Free)
            {
                throw new ArgumentException("Запись можно назначить только на свободный слот. Текущий статус: " + existingStatus, nameof(newStatus));
            }

            if (newStatus == Enums.AppointmentStatus.DoneNoPay && existingStatus != Enums.AppointmentStatus.Taken)
            {
                throw new ArgumentException("Завершение без оплаты возможно только для назначенной записи. Текущий статус: " + existingStatus, nameof(newStatus));
            }

            if (newStatus == Enums.AppointmentStatus.Cancelled && existingStatus != Enums.AppointmentStatus.Taken)
            {
                throw new ArgumentException("Отмена возможна только для назначенной записи. Текущий статус: " + existingStatus, nameof(newStatus));
            }

            if (newStatus == Enums.AppointmentStatus.DonePaid && existingStatus != Enums.AppointmentStatus.DoneNoPay)
            {
                throw new ArgumentException("Оплата возможна только после завершения без оплаты. Текущий статус: " + existingStatus, nameof(newStatus));
            }
        }

        public async Task<List<AppointmentDTO>> GetAllFreeByMedicAndServiceAsync(string serviceId, string medicId = null)
        {
            var threeWeeksFromNow = DateTime.UtcNow.Date.AddDays(21);
            Expression<Func<Appointment, bool>> filter = medicId == null ?
                (x => x.Status == DAL.Entities.AppointmentStatus.Free && x.ServiceId == serviceId
                && x.StartDate >= DateTime.UtcNow.Date && x.StartDate <= threeWeeksFromNow)
                : (x => x.Status == DAL.Entities.AppointmentStatus.Free && x.ServiceId == serviceId && x.MedicId == medicId
                && x.StartDate >= DateTime.UtcNow.Date && x.StartDate <= threeWeeksFromNow);

            var entities = await _repository.GetAllAsync(
                filter: filter,
                includes: [x => x.Service, x => x.Medic]);

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
