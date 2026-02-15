using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("Appointment")]
    public class Appointment : BaseEntity
    {
        [Required]
        [MaxLength(36)]
        public string PatientId { get; set; }

        [Required]
        [MaxLength(36)]
        public string ServiceId { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedicId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(20)]
        public AppointmentStatus Status { get; set; }
        public decimal TotalCost { get; set; } = decimal.Zero;
    }
}
