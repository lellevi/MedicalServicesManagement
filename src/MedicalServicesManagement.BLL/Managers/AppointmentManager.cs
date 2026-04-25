using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class AppointmentManager : BaseManager<AppointmentDTO, Appointment>, IAppointmentManager
    {
        public AppointmentManager(ISqlRepository<Appointment> repository, IMapper mapper)
            : base(repository, mapper)
        {
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

            Expression<Func<Appointment, bool>> filter = x =>
                x.Status == AppointmentStatus.Free &&
                x.ServiceId == serviceId &&
                x.StartDate >= DateTime.UtcNow.Date &&
                x.StartDate <= threeWeeksFromNow &&
                (medicId == null || x.MedicId == medicId);

            var entities = await _repository.GetAllAsync(filter, [x => x.Service, x => x.Medic]);
            entities = entities.OrderBy(a => a.StartDate.Date).ThenBy(a => a.StartDate.TimeOfDay).ToList();

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public override async Task CreateAsync(AppointmentDTO item)
        {
            Validate(item);

            var entity = _mapper.Map<Appointment>(item);

            entity.Status = AppointmentStatus.Free;

            await _repository.CreateAsync(entity);
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

        public async Task<List<AppointmentDTO>> GetAllIncludingServiceAndMedicAsync(
            string specialityId = null,
            string medicId = null,
            int? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            Expression<Func<Appointment, bool>> filter = x =>
                (string.IsNullOrEmpty(specialityId) || x.Service.MedSpecialityId == specialityId) &&
                (string.IsNullOrEmpty(medicId) || x.MedicId == medicId) &&
                (!status.HasValue || x.Status == (AppointmentStatus)status.Value) &&
                (!startDate.HasValue || x.StartDate.Date >= startDate.Value.Date) &&
                (!endDate.HasValue || x.EndDate.Date <= endDate.Value.Date);

            var entities = await _repository.GetAllAsync(
                filter: filter,
                includes: [x => x.Service, x => x.Medic]);

            entities = entities
                .OrderBy(a => a.StartDate.Date)
                .ThenBy(a => a.StartDate.TimeOfDay)
                .ToList();

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<List<AppointmentDTO>> GetAllPatientAppointmentsAsync(string id)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => (x.PatientId == id) && ((x.Status == AppointmentStatus.Taken) || (x.Status == AppointmentStatus.DoneNoPay)),
                includes: [x => x.Service, x => x.Medic]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<List<AppointmentDTO>> GetAllMedicAppointmentsAsync(string id)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => (x.MedicId == id) && ((x.Status == AppointmentStatus.Free) || (x.Status == AppointmentStatus.Taken)),
                includes: [x => x.Service, x => x.Medic, x => x.Patient]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<List<AppointmentDTO>> GetPatientHistoryAppointmentsAsync(string id)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => (x.PatientId == id) && (x.Status == AppointmentStatus.DonePaid),
                includes: [x => x.Service, x => x.Medic]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<List<AppointmentDTO>> GetMedicHistoryAppointmentsAsync(string id)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => (x.MedicId == id) && ((x.Status == AppointmentStatus.DoneNoPay) || (x.Status == AppointmentStatus.DonePaid)),
                includes: [x => x.Service, x => x.Medic, x => x.Patient]);

            return _mapper.Map<List<AppointmentDTO>>(entities);
        }

        public async Task<AppointmentDTO> GetByIdIncludingServiceAndMedicAsync(string id)
        {
            var entities = await _repository.GetSingleAsync(
                filter: x => x.Id == id,
                includes: [x => x.Service, x => x.Medic]);

            return _mapper.Map<AppointmentDTO>(entities);
        }

        public async Task MarkAsTakenAsync(string appointmentId, string patientId)
        {
            var appointment = await _repository.GetSingleAsync(x => x.Id == appointmentId);
            if (appointment.Status != AppointmentStatus.Free)
            {
                throw new ArgumentException("Данный талон занят, выберите другой");
            }

            appointment.Status = AppointmentStatus.Taken;
            appointment.PatientId = patientId;
            await _repository.UpdateAsync(appointment);
        }

        public async Task MarkAsFreeAsync(string appointmentId)
        {
            var appointment = await _repository.GetSingleAsync(x => x.Id == appointmentId);
            if (appointment.Status == AppointmentStatus.Free)
            {
                throw new ArgumentException("Данный талон уже свободен.");
            }

            appointment.Status = AppointmentStatus.Free;
            appointment.PatientId = null;
            await _repository.UpdateAsync(appointment);
        }

        public async Task<List<AppointmentDTO>> GetAllAsync(string specialityId)
        {
            var entities = await _repository.GetAllAsync(
                filter: x => x.Service.MedSpecialityId == specialityId,
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
