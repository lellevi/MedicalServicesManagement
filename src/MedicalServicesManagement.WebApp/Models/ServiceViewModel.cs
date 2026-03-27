using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class ServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool ForAdults { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        public MedSpecialityViewModel MedSpeciality { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
