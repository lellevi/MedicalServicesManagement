using Microsoft.AspNetCore.Identity;

namespace MedicalServicesManagement.DAL.Entities
{
    public class AuthUser : IdentityUser
    {
        public string ImageName { get; set; }
    }
}
