using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class SeviceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool ForAdults { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }

    }
}
