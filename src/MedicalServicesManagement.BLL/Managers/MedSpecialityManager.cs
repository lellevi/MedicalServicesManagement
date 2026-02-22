using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;

namespace MedicalServicesManagement.BLL.Managers
{
    public class MedSpecialityManager : BaseManager<MedSpecialityDTO, MedSpeciality>, IMedSpecialityManager
    {
        protected override string EntityName { get => "medSpeciality"; }

        public MedSpecialityManager(IRepository<MedSpeciality> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(MedSpecialityDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (item.Name.Length > 50)
                throw new ArgumentException("Name max length is 50");
        }
    }
}
