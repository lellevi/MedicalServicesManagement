using MedicalServicesManagement.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MedicalServicesManagement.DAL.Factories
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\MedicalServicesManagement.WebApp");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(assemblyPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AuthDB");

            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
