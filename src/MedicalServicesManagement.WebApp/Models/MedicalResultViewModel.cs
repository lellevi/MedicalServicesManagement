using System;

namespace MedicalServicesManagement.WebApp.Models
{
    public class MedicalResultViewModel
    {
        public AppointmentViewModel Appointment { get; set; }

        public string AppointmentId { get; set; }

        public UserViewModel Medic { get; set; }

        public UserViewModel Patient { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ExaminationData { get; set; }

        public string Diagnosis { get; set; }

        public string Recommendations { get; set; }
    }
}
