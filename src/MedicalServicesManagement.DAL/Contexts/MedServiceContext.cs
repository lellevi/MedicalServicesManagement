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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EntityUser>(entity =>
            {
                entity.HasOne(e => e.MedSpeciality)
                        .WithMany() // Is not Required
                        .HasForeignKey(e => e.MedSpecialityId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<EntityUser> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MedSpeciality> MedSpecialities { get; set; }
        public DbSet<AppointmentServiceDAL> AppointmentServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
    }
}
