using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.DAL.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [Required]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
