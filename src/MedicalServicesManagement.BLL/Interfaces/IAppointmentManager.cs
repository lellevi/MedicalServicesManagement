using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.BLL.Dto;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IAppointmentManager : IManager<AppointmentDTO>
    {
        public Task<List<AppointmentDTO>> GetAllFreeByMedicAndServiceAsync(string serviceId, string medicId = null);

        public Task<List<AppointmentDTO>> GetAllIncludingServiceAndMedicAsync();
    }
}
