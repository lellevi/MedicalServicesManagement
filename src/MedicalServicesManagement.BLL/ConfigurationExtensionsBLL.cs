using System.Collections.Generic;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.DAL;
using MedicalServicesManagement.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IMedSpecialityManager, MedSpecialityManager>();
            services.AddScoped<IAppointmentServiceManager, AppointmentServiceManager>();
            services.AddScoped<IAppointmentManager, AppointmentManager>();
            services.AddScoped<IAdditionalServiceManager, AdditionalServiceManager>();
        }
    }
}
