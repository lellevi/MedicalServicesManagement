using System;
using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.BLL.Managers
{
    internal class MedSpecialityManager : BaseManager<MedSpecialityDTO, MedSpeciality>
    {
        public MedSpecialityManager(ISqlRepository<MedSpeciality> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        protected override string EntityName { get => "medSpeciality"; }

        protected override void Validate(MedSpecialityDTO item)
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
        }
    }
}
