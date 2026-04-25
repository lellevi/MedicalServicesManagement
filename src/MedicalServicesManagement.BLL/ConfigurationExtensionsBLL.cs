using System.Collections.Generic;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.BLL.Managers;
using MedicalServicesManagement.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalServicesManagement.BLL
{
    public static class ConfigurationExtensionsBll
    {
        public static void ConfigureBLL(this IServiceCollection services, Dictionary<string, string> connectionStrings, string mongoDbName)
        {
            services.ConfigureDAL(connectionStrings, mongoDbName);

            services.AddScoped<JwtTokenService>();

            services.AddScoped<IManager<EntityUserDTO>, EntityUserManager>();
            services.AddScoped<IEntityUserManager, EntityUserManager>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IManager<ServiceDTO>, ServiceManager>();

            services.AddScoped<IManager<MedSpecialityDTO>, MedSpecialityManager>();
            services.AddScoped<IManager<AppointmentServiceDTO>, AppointmentServiceManager>();

            services.AddScoped<IAppointmentManager, AppointmentManager>();
            services.AddScoped<IManager<AppointmentDTO>, AppointmentManager>();

            services.AddScoped<IManager<AdditionalServiceDTO>, AdditionalServiceManager>();
        }
    }
}
