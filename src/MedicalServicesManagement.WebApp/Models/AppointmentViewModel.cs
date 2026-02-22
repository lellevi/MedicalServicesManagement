using MedicalServicesManagement.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }

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
        public AppointmentStatus Status { get; set; } // need add enum to web?

        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; }
    }
}
