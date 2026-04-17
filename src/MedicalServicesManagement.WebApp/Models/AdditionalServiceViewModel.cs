using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class AdditionalServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required]
        [MaxLength(36)]
        [Display(Name = "Область специализации")]
        public string MedSpecialityId { get; set; }

        public MedSpecialityViewModel MedSpeciality { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Стоимость")]
        public decimal Price { get; set; }
    }
}
