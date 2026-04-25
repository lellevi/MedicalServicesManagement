using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.BLL.Dto;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IServiceManager : IManager<ServiceDTO>
    {
        Task<List<ServiceDTO>> GetAllIncludingSpecialitiesAsync();

        Task<List<ServiceDTO>> GetByMedSpecialityIdAsync(string id);
    }
}
