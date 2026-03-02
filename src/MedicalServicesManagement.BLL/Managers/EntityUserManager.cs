using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public interface IEntityUserManager : IManager<EntityUserDTO, EntityUser>
    {
        public Task<EntityUserDTO> GetByAuthIdAsync(string id);
        public Task<List<EntityUserDTO>> GetMedicsAsync();
        public Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname);
        public Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId);
    }

    public class EntityUserManager : BaseManager<EntityUserDTO, EntityUser>, IEntityUserManager
    {
        protected override string EntityName { get => "user"; }

        public EntityUserManager(IRepository<EntityUser> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(EntityUserDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.AuthUserId))
                throw new ArgumentException("AuthUserId is required.");
            if (item.AuthUserId.Length > 36)
                throw new ArgumentException("AuthUserId max length is 36.");
            if (!string.IsNullOrEmpty(item.MedInfo) && item.MedInfo.Length > 50)
                throw new ArgumentException("MedInfo max length is 50.");
        }

        public async Task<EntityUserDTO> GetByAuthIdAsync(string id)
        {
            var entity = await _repository.GetSingleAsync(x => x.AuthUserId == id);

            return _mapper.Map<EntityUserDTO>(entity);
        }

        public async Task<List<EntityUserDTO>> GetMedicsAsync()
        {
            var entities = await _repository.GetAllAsync(x => x.MedSpecialityId != null,
                includes: [ u => u.MedSpeciality ]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }

        public async Task<List<EntityUserDTO>> GetMedicsBySurnameAsync(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                return await GetMedicsAsync();
            }

            var entities = await _repository.GetAllAsync(x => x.MedSpecialityId != null && x.Surname == surname,
                includes: [u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }

        public async Task<List<EntityUserDTO>> GetMedicsBySpecialityAsync(string specialityId)
        {
            var entities = await _repository.GetAllAsync(x => x.MedSpecialityId != null && x.MedSpecialityId == specialityId,
                includes: [u => u.MedSpeciality]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }
    }
}
