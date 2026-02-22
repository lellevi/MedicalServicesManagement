using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
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

        public async Task<List<EntityUserDTO>> GetMedicsAsync()
        {
            var entities = await _repository.GetAllAsync(x => x.MedSpecialityId != null,
                includes: [ u => u.MedSpeciality ]);

            return _mapper.Map<List<EntityUserDTO>>(entities);
        }
    }
}
