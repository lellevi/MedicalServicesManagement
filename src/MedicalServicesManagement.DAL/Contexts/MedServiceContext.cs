using MedicalServicesManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalServicesManagement.DAL.Contexts
{
    public class MedServiceContext : DbContext
    {
        public MedServiceContext(DbContextOptions<MedServiceContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MedSpeciality> MedSpecialities { get; set; }
        public DbSet<AppointmentService> AppointmentServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
    }
}
