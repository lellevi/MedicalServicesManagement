using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class ServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Название услуги")]
        public string Name { get; set; }

        [Display(Name = "Для взрослых")]
        public bool ForAdults { get; set; }

        [Required]
        [MaxLength(36)]
        [Display(Name = "Специальность")]
        public string MedSpecialityId { get; set; }

        public MedSpecialityViewModel MedSpeciality { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Стоимость", Description = "0.01")]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        [Display(Name = "Дополнительная информация")]
        public string Comment { get; set; }
    }
}
