using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AppointmentServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string AdditionalServiceId { get; set; }

        [Required]
        [MaxLength(36)]
        public string AppointmentId { get; set; }

        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
