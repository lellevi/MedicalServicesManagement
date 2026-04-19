using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.BLL.Dto;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IAppointmentManager : IManager<AppointmentDTO>
    {
        public Task<List<AppointmentDTO>> GetAllIncludingServiceAndMedicAsync();

        public Task<List<AppointmentDTO>> GetAllAsync(string specialityId);

        public Task<List<AppointmentDTO>> GetAllFreeByMedicAndServiceOrderedAsync(string serviceId, string medicId = null);
    }
}
