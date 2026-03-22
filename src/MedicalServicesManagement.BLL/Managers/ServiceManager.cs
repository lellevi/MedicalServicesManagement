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
    public interface IServiceManager : IManager<ServiceDTO, Service>
    {
        Task<List<ServiceDTO>> GetAllWithSpecialitiesAsync();
    }

    public class ServiceManager : BaseManager<ServiceDTO, Service>, IServiceManager
    {
        protected override string EntityName { get => "service"; }

        public ServiceManager(IRepository<Service> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(ServiceDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (item.Name.Length > 50)
                throw new ArgumentException("Name max length is 50");
            if (string.IsNullOrEmpty(item.MedSpecialityId) || item.MedSpecialityId.Length > 36)
                throw new ArgumentException("MedSpecialityId is required and max length 36");
            if (item.Cost < 0)
                throw new ArgumentException("Cost must be zero or positive");
        }

        public async Task<List<ServiceDTO>> GetAllWithSpecialitiesAsync()
        {
            var entities = await _repository.GetAllAsync(includes: [x => x.MedSpeciality]);

            return _mapper.Map<List<ServiceDTO>>(entities);
        }
    }
}
