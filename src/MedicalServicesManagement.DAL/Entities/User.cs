using Microsoft.AspNetCore.Identity;

namespace MedicalServicesManagement.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; }
        public int? MedSpecialityId {  get; set; }
        public string? MedInfo { get; set; }
    }
}
