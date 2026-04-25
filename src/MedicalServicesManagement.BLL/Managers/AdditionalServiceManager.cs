using System;
using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class AdditionalServiceManager : BaseManager<AdditionalServiceDTO, AdditionalService>
    {
        public AdditionalServiceManager(IRepository<AdditionalService> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override string EntityName { get => "additionalService"; }

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
