using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.BLL.Dto;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IEntityUserManager : IManager<EntityUserDTO>
    {
        public Task<List<EntityUserDTO>> GetAllIncludeMedSpecialitiesAsync();

        public Task<EntityUserDTO> GetByAuthIdAsync(string id);

        public Task<List<EntityUserDTO>> GetMedicsAsync();

        public Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname);

        public Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId);

        public Task<Dictionary<string, List<EntityUserDTO>>> GetAllByRolesAsync(List<string> roles);

        public Task<EntityUserDTO> GetByIdIncludingRoles(string id);
    }
}
