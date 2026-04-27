using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("Appointment")]
    public class Appointment : BaseSqlEntity
    {
        [MaxLength(36)]
        public string PatientId { get; set; }

        public virtual EntityUser Patient { get; set; }

        [Required]
        [MaxLength(36)]
        public string ServiceId { get; set; }

        public virtual Service Service { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedicId { get; set; }

        public virtual EntityUser Medic { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public AppointmentStatus Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; } = decimal.Zero;
    }
}
