using MedicalServicesManagement.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MedicalServicesManagement.DAL.Factories
{
    public class MedServiceContextFactory : IDesignTimeDbContextFactory<MedServiceContext>
    {
        public MedServiceContext CreateDbContext(string[] args)
        {
            var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\MedicalServicesManagement.WebApp");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(assemblyPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MedDB");

            var optionsBuilder = new DbContextOptionsBuilder<MedServiceContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MedServiceContext(optionsBuilder.Options);
        }
    }
}
