using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.DAL;
using MedicalServicesManagement.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MedicalServicesManagement.BLL
{
    public static class ConfigurationExtensionsBLL
    {
        public static void ConfigureBLL(this IServiceCollection services, Dictionary<string, string> connectionStrings)
        {
            services.ConfigureDAL(connectionStrings);

            services.AddScoped<JwtTokenService>();

            services.AddScoped<IManager<EntityUserDTO, EntityUser>, EntityUserManager>();
            services.AddScoped<IEntityUserManager, EntityUserManager>();

            services.AddScoped<IManager<ServiceDTO, Service>, ServiceManager>();
            services.AddScoped<IManager<MedSpecialityDTO, MedSpeciality>, MedSpecialityManager>();
            services.AddScoped<IManager<AppointmentServiceDTO, AppointmentServiceDAL>, AppointmentServiceManager>();
            services.AddScoped<IManager<AppointmentDTO, Appointment>, AppointmentManager>();
            services.AddScoped<IManager<AdditionalServiceDTO, AdditionalService>, AdditionalServiceManager>();
        }
    }
}
