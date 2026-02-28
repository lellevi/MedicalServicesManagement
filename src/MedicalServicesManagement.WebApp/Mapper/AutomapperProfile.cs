using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;

namespace MedicalServicesManagement.WebApp.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<EntityUser, EntityUserDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<MedSpeciality, MedSpecialityDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<AppointmentService, AppointmentServiceDTO>().ReverseMap();
            CreateMap<AdditionalService, AdditionalServiceDTO>().ReverseMap();

            CreateMap<BLL.Enums.AppointmentStatus, Enums.AppointmentStatus>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
        }
    }
}
