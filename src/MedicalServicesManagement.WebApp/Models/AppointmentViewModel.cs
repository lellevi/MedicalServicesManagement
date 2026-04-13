using MedicalServicesManagement.WebApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }

        [MaxLength(36)]
        public string PatientId { get; set; }

        public UserViewModel Patient { get; set; }

        [Required]
        [MaxLength(36)]
        public string ServiceId { get; set; }

        public ServiceViewModel Service { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedicId { get; set; }

        public UserViewModel Medic { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        public AppointmentStatus? Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; }
    }
}
