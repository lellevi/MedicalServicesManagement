using MedicalServicesManagement.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IAppointmentManager : IManager<AppointmentDTO>
    {
        public Task<List<AppointmentDTO>> GetAllIncludingServiceAndMedicAsync();

        public Task<List<AppointmentDTO>> GetAllPatientAppointmentsAsync(string id);

        public Task<List<AppointmentDTO>> GetAllMedicAppointmentsAsync(string id);

        public Task<List<AppointmentDTO>> GetPatientHistoryAppointmentsAsync(string id);

        public Task<List<AppointmentDTO>> GetMedicHistoryAppointmentsAsync(string id);

        public Task<AppointmentDTO> GetByIdIncludingServiceAndMedicAsync(string id);

        public Task<List<AppointmentDTO>> GetFilteredAppointmentsAsync(string specialityId, string medicId, Enums.AppointmentStatus? status, DateTime? startDate, DateTime? endDate);

        public Task MarkAsTakenAsync(string appointmentId, string patientId);

        public Task MarkAsFreeAsync(string appointmentId);

        public Task<List<AppointmentDTO>> GetAllAsync(string specialityId);

        public Task<List<AppointmentDTO>> GetAllFreeByMedicAndServiceOrderedAsync(string serviceId, string medicId = null);
    }
}
