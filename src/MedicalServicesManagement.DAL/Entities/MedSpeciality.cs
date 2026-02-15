using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServicesManagement.DAL.Entities
{
    [Table("MedSpeciality")]
    public class MedSpeciality : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
