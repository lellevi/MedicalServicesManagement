using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using MedicalServicesManagement.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MedicalServicesManagement.DAL
{
    public static class ConfigurationExtensionsDAL
    {
        public const string MedConnectionString = "MedDB";
        public const string AuthConnectionString = "AuthDB";
        public static void ConfigureDAL(this IServiceCollection services, Dictionary<string, string> connectionStrings)
        {
            var medString = connectionStrings.GetValueOrDefault(MedConnectionString)
                ?? throw new ArgumentNullException(MedConnectionString);
            var authString = connectionStrings.GetValueOrDefault(AuthConnectionString)
                ?? throw new ArgumentNullException(AuthConnectionString);

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString: authString));
            services.AddIdentity<AuthUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<MedServiceContext>(options =>
                options.UseSqlServer(connectionString: medString));

            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            services.AddScoped<IRepository<Service>, GenericRepository<Service>>();
            services.AddScoped<IRepository<MedSpeciality>, GenericRepository<MedSpeciality>>();
            services.AddScoped<IRepository<AppointmentServiceDAL>, GenericRepository<AppointmentServiceDAL>>();
            services.AddScoped<IRepository<Appointment>, GenericRepository<Appointment>>();
            services.AddScoped<IRepository<AdditionalService>, GenericRepository<AdditionalService>>();
        }
    }
}
