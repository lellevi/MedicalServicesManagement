using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("Service")]
    public class Service : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool ForAdults { get; set; } = true;

        [Required]
        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; } = decimal.Zero;

        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
