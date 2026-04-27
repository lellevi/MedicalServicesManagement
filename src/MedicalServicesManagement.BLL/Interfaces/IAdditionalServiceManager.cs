using MedicalServicesManagement.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IAdditionalServiceManager : IManager<AdditionalServiceDTO>
    {
        public Task<List<AdditionalServiceDTO>> GetAllIncludingSpecialitiesAsync();
    }
}
