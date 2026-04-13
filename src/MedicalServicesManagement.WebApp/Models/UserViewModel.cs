using System;
using System.Collections.Generic;
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
        [Display(Name = "Медицинская специальность")]
        public string MedSpecialityId { get; set; }

        public MedSpecialityViewModel MedSpeciality { get; set; }

        [MaxLength(50)]
        [Display(Name = "Медицинская информация", Prompt = "Стаж, квалификация...")]
        public string MedInfo { get; set; }

        [Display(Description = "Роли")]
        public List<string> Roles { get; set; }

        public List<ServiceViewModel> Services { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        public string MiddleNameInitial => string.IsNullOrEmpty(MiddleName) ? string.Empty : $" {MiddleName}";

        public string FullName => $"{Surname} {Name}{MiddleNameInitial}";

        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Мобильный телефон", Description = "+375()")]
        public string Telephone { get; set; }
    }
}
