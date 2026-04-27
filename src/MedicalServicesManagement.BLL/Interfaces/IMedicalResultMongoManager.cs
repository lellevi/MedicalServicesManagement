using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IMedicalResultMongoManager : IMongoBaseManager<MedicalResult, MedicalResultDto>
    {
        public Task<MedicalResultDto> GetByAppointmentIdAsync(string appointmentId);

        public Task<List<MedicalResultDto>> GetByPatientIdAsync(string patientId);

        public Task CreateOrUpdateByAppointmentIdAsync(string appointmentId, MedicalResultDto dto);
    }
}
