using System.ComponentModel.DataAnnotations;

namespace MedicalServicesManagement.WebApp.Models.Auth
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Must be filled")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Must be filled")]
        public string Password { get; set; }
    }
}
