using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("AdditionalService")]
    public class AdditionalService : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(36)]
        public string MedSpecialityId { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
