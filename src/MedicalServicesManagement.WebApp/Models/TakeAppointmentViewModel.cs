using System;
using System.Collections.Generic;

namespace MedicalServicesManagement.WebApp.Models
{
    public class TakeAppointmentViewModel
    {
        public UserViewModel Medic { get; set; }

        public ServiceViewModel Service { get; set; }

        public Dictionary<DateTime, List<AppointmentViewModel>> AppointmentsByDate { get; set; }
    }
}
