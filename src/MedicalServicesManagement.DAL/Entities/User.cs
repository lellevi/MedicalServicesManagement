using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("[User]")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(36)]
        public string AuthUserId { get; set; }

        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        [MaxLength(50)]
        public string MedInfo { get; set; }
    }

    public class AuthUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        public string MiddleNameInitial => string.IsNullOrEmpty(MiddleName) ? string.Empty : $" {MiddleName}";

        public string FullName => $"{Surname} {Name}{MiddleNameInitial}";

        [Required]
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; }
    }
}
