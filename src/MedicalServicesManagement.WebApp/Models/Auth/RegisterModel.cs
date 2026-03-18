using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models.Auth
{
    public class RegisterModel
    {
        [Display(Name = "Email", Description = "Email")]
        [Required(ErrorMessage = "Must be filled")]
        public string Email { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Must be filled")]
        public string Surname { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Must be filled")]
        public string Name { get; set; }

        [Display(Name = "MiddleName")]
        public string MiddleName { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Must be filled")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Must be filled")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Must be filled")]
        public string ConfirmPassword { get; set; }
    }
}
