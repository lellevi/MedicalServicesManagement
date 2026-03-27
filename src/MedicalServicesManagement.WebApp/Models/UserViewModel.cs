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

        public MedSpecialityViewModel MedSpeciality { get; set; }

        [MaxLength(50)]
        public string MedInfo { get; set; }

        public List<string> Roles { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string MiddleNameInitial => string.IsNullOrEmpty(MiddleName) ? string.Empty : $" {MiddleName}";

        public string FullName => $"{Surname} {Name}{MiddleNameInitial}";

        public DateTime BirthDate { get; set; }

        public string Telephone { get; set; }
    }
}
