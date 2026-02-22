using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;

namespace MedicalServicesManagement.BLL.Mapper
{
    public class BLLAutomapperProfile : Profile
    {
        public BLLAutomapperProfile()
        {
            CreateMap<EntityUser, EntityUserDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<MedSpeciality, MedSpecialityDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<AppointmentServiceDAL, AppointmentServiceDTO>().ReverseMap();
            CreateMap<AdditionalService, AdditionalServiceDTO>().ReverseMap();
        }
    }
}
