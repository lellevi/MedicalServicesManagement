using MedicalServicesManagement.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IAppointmentServiceManager : IManager<AppointmentServiceDTO>
    {
        Task<List<AppointmentServiceDTO>> GetByAppointmentIdAsync(string appointmentId);

        Task<List<AppointmentServiceDTO>> GetAllIncludingSpecialitiesAsync();
    }
}
