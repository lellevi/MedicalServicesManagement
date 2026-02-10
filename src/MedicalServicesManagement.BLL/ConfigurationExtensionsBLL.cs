using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.DAL;
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
            //services.AddScoped<IShowService, ShowService>();
            //services.AddScoped<ITicketService, TicketService>();
        }
    }
}
