using System;
using System.ComponentModel.DataAnnotations;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.DAL.Entities
{
    public abstract class BaseSqlEntity : IEntity
    {
        [Key]
        [Required]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
