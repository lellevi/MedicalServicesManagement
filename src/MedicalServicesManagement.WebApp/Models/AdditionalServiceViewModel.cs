using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AdditionalServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
