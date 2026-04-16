using System;
using System.Collections.Generic;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using MedicalServicesManagement.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalServicesManagement.DAL
{
    public static class ConfigurationExtensionsDAL
    {
        public const string MedConnectionString = "MedDB";
        public const string AuthConnectionString = "AuthDB";

        public static void ConfigureDAL(this IServiceCollection services, Dictionary<string, string> connectionStrings)
        {
            var medString = connectionStrings.GetValueOrDefault(MedConnectionString)
                ?? throw new ArgumentException($"Error connection to '{MedConnectionString}'.", nameof(connectionStrings));
            var authString = connectionStrings.GetValueOrDefault(AuthConnectionString)
                ?? throw new ArgumentException($"Error connection to '{AuthConnectionString}'.", nameof(connectionStrings));

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString: authString));

            services.AddIdentityCore<AuthUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<MedServiceContext>(options =>
                options.UseSqlServer(connectionString: medString));

            services.AddScoped<IRepository<EntityUser>, GenericRepository<EntityUser>>();
            services.AddScoped<IEntityUserRepository, EntityUserRepository>();

            services.AddScoped<IRepository<Service>, GenericRepository<Service>>();
            services.AddScoped<IRepository<MedSpeciality>, GenericRepository<MedSpeciality>>();

            services.AddScoped<IRepository<AppointmentService>, GenericRepository<AppointmentService>>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<IRepository<Appointment>, GenericRepository<Appointment>>();
            services.AddScoped<IRepository<AdditionalService>, GenericRepository<AdditionalService>>();
        }
    }
}
