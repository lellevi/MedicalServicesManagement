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
    public class UserManager : BaseManager<UserDTO, User>, IUserManager
    {
        protected override string EntityName { get => "user"; }

        public UserManager(IRepository<User> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(UserDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.AuthUserId))
                throw new ArgumentException("AuthUserId is required.");
            if (item.AuthUserId.Length > 36)
                throw new ArgumentException("AuthUserId max length is 36.");
            if (!string.IsNullOrEmpty(item.MedInfo) && item.MedInfo.Length > 50)
                throw new ArgumentException("MedInfo max length is 50.");
        }

        public async Task<IReadOnlyCollection<UserDTO>> GetMedicsAsync()
        {
            var entities = await _repository.GetAllAsync(x => x.MedSpecialityId != null);
            return _mapper.Map<IReadOnlyCollection<UserDTO>>(entities);
        }
    }
}
