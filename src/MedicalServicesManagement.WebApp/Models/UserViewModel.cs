using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string AuthUserId { get; set; }

        [MaxLength(36)]
        public string MedSpecialityId { get; set; }

        [MaxLength(50)]
        public string MedInfo { get; set; }

        public List<string> Roles { get; set; }
    }
}
