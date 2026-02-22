using MedicalServicesManagement.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace MedicalServicesManagement.DAL.Contexts
{
    public partial class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "1";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = Constants.AdminRole, NormalizedName = Constants.AdminRole.ToUpper() },
                new IdentityRole { Id = "2", Name = Constants.PatientRole, NormalizedName = Constants.PatientRole.ToUpper() },
                new IdentityRole { Id = "3", Name = Constants.MedicRole, NormalizedName = Constants.MedicRole.ToUpper() },
                new IdentityRole { Id = "4", Name = Constants.ReceptionistRole, NormalizedName = Constants.ReceptionistRole.ToUpper() }
            );

            var adminUser = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = Constants.AdminMailbox,
                Surname = Constants.AdminSurname,
                Name = Constants.AdminName,
                Email = Constants.AdminMailbox,
                NormalizedUserName = Constants.AdminMailbox,
                NormalizedEmail = Constants.AdminMailbox,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var hasher = new PasswordHasher<AuthUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, Constants.AdminInitialPassword);

            builder.Entity<AuthUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUser.Id
            });
        }
    }
}
