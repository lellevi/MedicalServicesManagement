using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.WebApp.Models;

namespace MedicalServicesManagement.WebApp.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserViewModel, EntityUserDTO>().ReverseMap();
            CreateMap<ServiceViewModel, ServiceDTO>().ReverseMap();
            CreateMap<MedSpecialityViewModel, MedSpecialityDTO>().ReverseMap();
            CreateMap<AppointmentViewModel, AppointmentDTO>().ReverseMap();
            CreateMap<AppointmentServiceViewModel, AppointmentServiceDTO>().ReverseMap();
            CreateMap<AdditionalServiceViewModel, AdditionalServiceDTO>().ReverseMap();

            CreateMap<MedicalResultViewModel, MedicalResultDto>().ReverseMap();

            CreateMap<BLL.Enums.AppointmentStatus, Enums.AppointmentStatus>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
        }
    }
}
