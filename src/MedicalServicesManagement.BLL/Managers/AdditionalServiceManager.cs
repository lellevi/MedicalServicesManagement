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
    internal class AdditionalServiceManager : BaseManager<AdditionalServiceDTO, AdditionalService>, IAdditionalServiceManager
    {
        public AdditionalServiceManager(ISqlRepository<AdditionalService> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override string EntityName { get => "additionalService"; }

        public async Task<List<AdditionalServiceDTO>> GetAllIncludingSpecialitiesAsync()
        {
            var entities = await _repository.GetAllAsync(includes: [x => x.MedSpeciality]);

            return _mapper.Map<List<AdditionalServiceDTO>>(entities);
        }

        protected override void Validate(AdditionalServiceDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Name is required");
            }

            if (item.Name.Length > 50)
            {
                throw new ArgumentException("Name max length is 50");
            }

            if (string.IsNullOrEmpty(item.MedSpecialityId) || item.MedSpecialityId.Length > 36)
            {
                throw new ArgumentException("MedSpecialityId is required and max length 36");
            }

            if (item.Price < 0)
            {
                throw new ArgumentException("Price must be zero or positive");
            }
        }
    }
}
