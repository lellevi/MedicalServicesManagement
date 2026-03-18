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

            modelBuilder.Entity<AppointmentService>(entity =>
            {
                entity.HasOne(e => e.AdditionalService)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.AdditionalServiceId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Appointment)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.AppointmentId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AdditionalService>(entity =>
            {
                entity.HasOne(e => e.MedSpeciality)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.MedSpecialityId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasOne(e => e.MedSpeciality)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.MedSpecialityId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(e => e.Patient)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Service)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.ServiceId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Medic)
                        .WithMany()
                        .IsRequired()
                        .HasForeignKey(e => e.MedicId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<EntityUser> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MedSpeciality> MedSpecialities { get; set; }
        public DbSet<AppointmentService> AppointmentServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
    }
}
