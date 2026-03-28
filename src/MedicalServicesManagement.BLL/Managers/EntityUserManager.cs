using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using MedicalServicesManagement.DAL.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MedicalServicesManagement.BLL.Managers
{
    public interface IEntityUserManager : IManager<EntityUserDTO, EntityUser>
    {
        public Task<List<EntityUserDTO>> GetAllIncludeMedSpecialitiesAsync();

        public Task<EntityUserDTO> GetByAuthIdAsync(string id);

        public Task<List<EntityUserDTO>> GetMedicsAsync();

        public Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname);

        public Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId);

        public Task<Dictionary<string, List<EntityUserDTO>>> GetAllByRolesAsync(List<string> roles);

        public Task<EntityUserDTO> GetByIdIncludingRoles(string id);
    }

    public class EntityUserManager : BaseManager<EntityUserDTO, EntityUser>, IEntityUserManager
    {
        private readonly IEntityUserRepository _userRepository;

        public EntityUserManager(IEntityUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _userRepository = repository;
        }

        protected override string EntityName { get => "user"; }

        protected override void Validate(EntityUserDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (string.IsNullOrWhiteSpace(item.AuthUserId))
            {
                throw new ArgumentException("AuthUserId is required.");
            }

            if (item.AuthUserId.Length > 36)
            {
                throw new ArgumentException("AuthUserId max length is 36.");
            }

            if (!string.IsNullOrEmpty(item.MedInfo) && item.MedInfo.Length > 50)
            {
                throw new ArgumentException("MedInfo max length is 50.");
            }
        }

        public async Task<Dictionary<string, List<EntityUserDTO>>> GetAllByRolesAsync(List<string> roles)
        {
            Dictionary<string, List<EntityUserDTO>> users = [];
            var entityUsers = await _repository.GetAllAsync(null, [u => u.MedSpeciality]);

            foreach (var role in roles)
            {
                var authUsers = await _userRepository.GetAuthUsersByRoleAsync(role);
                List<EntityUser> listUsers = authUsers.Select(x => entityUsers.FirstOrDefault(e => e.AuthUserId == x.Id)).ToList();

                List<EntityUserDTO> value = _mapper.Map<List<EntityUserDTO>>(listUsers);
                users.Add(role, value);
            }

            return users;
        }

        public override async Task UpdateAsync(EntityUserDTO item)
        {
            try
            {
                Validate(item);

                var entity = _mapper.Map<EntityUser>(item);
                await _userRepository.UpdateAsync(entity);
                await _userRepository.UpdateRoles(item.AuthUserId, item.Roles);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException($"Error updating {EntityName}: {ex.Message}", ex);
            }
        }

        public async Task<EntityUserDTO> GetByIdIncludingRoles(string id)
        {
            var roles = await _userRepository.GetUserRolesAsync(id);
            var entity = await _repository.GetSingleAsync(x => x.Id == id, null);
            var user = _mapper.Map<EntityUserDTO>(entity);
            user.Roles = roles;
            return user;
        }

        public async Task<List<EntityUserDTO>> GetAllIncludeMedSpecialitiesAsync()
        {
            var entities = await _repository.GetAllAsync(null, [u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }

        public async Task<EntityUserDTO> GetByAuthIdAsync(string id)
        {
            var entity = await _repository.GetSingleAsync(x => x.AuthUserId == id);

            return _mapper.Map<EntityUserDTO>(entity);
        }

        public async Task<List<EntityUserDTO>> GetMedicsAsync()
        {
            var entities = await _repository.GetAllAsync(
                x => x.MedSpecialityId != null,
                includes: [ u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }

        public async Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                return await GetMedicsAsync();
            }

            var entities = await _repository.GetAllAsync(
                x => x.MedSpecialityId != null && x.Surname == surname,
                includes: [u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }

        public async Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId)
        {
            var entities = await _repository.GetAllAsync(
                x => x.MedSpecialityId != null && x.MedSpecialityId == specialityId,
                includes: [u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }
    }
}
