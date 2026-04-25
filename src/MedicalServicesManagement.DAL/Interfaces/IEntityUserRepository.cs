using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Entities;

namespace MedicalServicesManagement.DAL.Interfaces
{
    public interface IEntityUserRepository : IRepository<EntityUser>
    {
        public Task UpdateRoles(string authUserId, List<string> newRoles);

        public Task<List<AuthUser>> GetAuthUsersByRoleAsync(string role);

        public Task<List<string>> GetUserRolesAsync(string id);
    }
}
