using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("AppointmentService")]
    public class AppointmentService : BaseEntity
    {
        [Required]
        [MaxLength(36)]
        public string AdditionalServiceId { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }

        [Required]
        [MaxLength(36)]
        public string AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        [Range(1, int.MaxValue)]
        public int Amount { get; set; } = 1;
    }
}
