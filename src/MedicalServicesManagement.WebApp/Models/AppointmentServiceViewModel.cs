using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AppointmentServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string AdditionalServiceId { get; set; }
        public AdditionalServiceViewModel AdditionalService { get; set; }

        [Required]
        [MaxLength(36)]
        public string AppointmentId { get; set; }
        public AppointmentViewModel Appointment { get; set; }


        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}
