using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.BLL.Dto;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IEntityUserManager : IManager<EntityUserDTO>
    {
        public Task<List<EntityUserDTO>> GetAllIncludeMedSpecialitiesAsync();

        public Task<List<EntityUserDTO>> GetAllByRoleAsync(string role);

        public Task<EntityUserDTO> GetByAuthIdAsync(string id, bool neededRoles = false);

        public Task<List<EntityUserDTO>> GetMedicsAsync();

        public Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname);

        public Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId);

        public Task<EntityUserDTO> GetByIdIncludingRoles(string id);
    }
}
