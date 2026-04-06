using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MedicalServicesManagement.DAL.Repositories
{
    public interface IEntityUserRepository : IRepository<EntityUser>
    {
        public Task UpdateRoles(string authUserId, List<string> newRoles);

        public Task<List<AuthUser>> GetAuthUsersByRoleAsync(string role);

        public Task<List<string>> GetUserRolesAsync(string id);
    }

    public class EntityUserRepository : GenericRepository<EntityUser>, IEntityUserRepository
    {
        private readonly UserManager<AuthUser> _authUserManager;

        public EntityUserRepository(MedServiceContext context, UserManager<AuthUser> authUserManager)
            : base(context)
        {
            _authUserManager = authUserManager;
        }

        public async Task UpdateRoles(string authUserId, List<string> newRoles)
        {
            var authUser = await _authUserManager.FindByIdAsync(authUserId);
            var userRoles = await _authUserManager.GetRolesAsync(authUser);

            var rolesToAdd = newRoles.Except(userRoles);
            var rolesToRemove = userRoles.Except(newRoles);

            await _authUserManager.AddToRolesAsync(authUser, rolesToAdd);
            await _authUserManager.RemoveFromRolesAsync(authUser, rolesToRemove);
        }

        public async Task<List<AuthUser>> GetAuthUsersByRoleAsync(string role)
        {
            var authUsers = await _authUserManager.GetUsersInRoleAsync(role);

            return authUsers.ToList();
        }

        public async Task<List<string>> GetUserRolesAsync(string id)
        {
            var user = await GetSingleAsync(x => x.Id == id);
            var authUser = await _authUserManager.FindByIdAsync(user.AuthUserId);
            var roles = await _authUserManager.GetRolesAsync(authUser);
            return roles.ToList();
        }
    }
}
