using System;
using System.Collections.Generic;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Factories;
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
        public const string MongoConnectionString = "MongoDB";

        public static void ConfigureDAL(this IServiceCollection services, Dictionary<string, string> connectionStrings, string mongoDbName)
        {
            var medString = connectionStrings.GetValueOrDefault(MedConnectionString)
                ?? throw new ArgumentException($"Error connection to '{MedConnectionString}'.", nameof(connectionStrings));
            var authString = connectionStrings.GetValueOrDefault(AuthConnectionString)
                ?? throw new ArgumentException($"Error connection to '{AuthConnectionString}'.", nameof(connectionStrings));
            var mongoString = connectionStrings.GetValueOrDefault(MongoConnectionString)
                ?? throw new ArgumentException($"Error connection to '{MongoConnectionString}'.", nameof(connectionStrings));

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString: authString));

            services.AddIdentityCore<AuthUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<MedServiceContext>(options =>
                options.UseSqlServer(connectionString: medString));

            services.AddScoped<ISqlRepository<EntityUser>, GenericRepository<EntityUser>>();
            services.AddScoped<IEntityUserRepository, EntityUserRepository>();

            services.AddScoped<ISqlRepository<Service>, GenericRepository<Service>>();
            services.AddScoped<ISqlRepository<MedSpeciality>, GenericRepository<MedSpeciality>>();

            services.AddScoped<ISqlRepository<AppointmentService>, GenericRepository<AppointmentService>>();

            services.AddScoped<ISqlRepository<Appointment>, GenericRepository<Appointment>>();
            services.AddScoped<ISqlRepository<AdditionalService>, GenericRepository<AdditionalService>>();

            services.AddSingleton<IMongoDbFactory>(new MongoDbFactory(mongoString, mongoDbName));
            services.AddTransient<IMongoRepository<MedicalResult>, MedicalResultRepository>();
        }
    }
}
