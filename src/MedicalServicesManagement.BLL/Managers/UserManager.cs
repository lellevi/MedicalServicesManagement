using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserManager(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private void Validate(UserDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.AuthUserId))
                throw new ArgumentException("AuthUserId is required.");
            if (item.AuthUserId.Length > 36)
                throw new ArgumentException("AuthUserId max length is 36.");
            if (!string.IsNullOrEmpty(item.MedInfo) && item.MedInfo.Length > 50)
                throw new ArgumentException("MedInfo max length is 50.");
        }

        public async Task CreateAsync(UserDTO item)
        {
            try
            {
                Validate(item);
                var entity = _mapper.Map<User>(item);
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating user: " + ex.Message, ex);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("User not found.");

            await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var entities = await (await _repository.GetAllAsync()).ToListAsync();
            return _mapper.Map<List<UserDTO>>(entities);
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<UserDTO>(entity);
        }

        public async Task UpdateAsync(UserDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Validate(item);

            var existing = await _repository.GetByIdAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"User not found.");

            var entity = _mapper.Map<User>(item);
            await _repository.UpdateAsync(entity);
        }
    }
}
