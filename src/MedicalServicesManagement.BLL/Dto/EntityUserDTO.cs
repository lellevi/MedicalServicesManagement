using System;
using System.Collections.Generic;

namespace MedicalServicesManagement.BLL.Dto
{
    public class EntityUserDTO : BaseDTO
    {
        public string AuthUserId { get; set; }

        public string MedSpecialityId { get; set; }

        public MedSpecialityDTO MedSpeciality { get; set; }

        public string MedInfo { get; set; }

        public List<string> Roles { get; set; }

        public string Role { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string MiddleNameInitial => string.IsNullOrEmpty(MiddleName) ? string.Empty : $" {MiddleName}";

        public string FullName => $"{Surname} {Name}{MiddleNameInitial}";

        public DateTime BirthDate { get; set; }

        public string Telephone { get; set; }
    }
}
