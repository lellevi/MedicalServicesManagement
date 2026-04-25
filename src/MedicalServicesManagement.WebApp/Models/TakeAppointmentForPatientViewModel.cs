using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalServicesManagement.WebApp.Models
{
    public class TakeAppointmentForPatientViewModel
    {
        public string MedicId { get; set; }

        public UserViewModel Medic { get; set; }

        public string PatientId { get; set; }

        public UserViewModel Patient { get; set; }

        public string ServiceId { get; set; }

        public ServiceViewModel Service { get; set; }

        public List<ServiceViewModel> Services { get; set; }

        public Dictionary<DateTime, List<AppointmentViewModel>> AppointmentsByDate { get; set; }
    }
}
