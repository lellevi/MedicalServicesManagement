using System.Collections.Generic;

namespace MedicalServicesManagement.WebApp.Models
{
    public class TakeAppointmentViewModel
    {
        public UserViewModel Medic { get; set; }

        public ServiceViewModel Service { get; set; }

        public List<AppointmentViewModel> AvailableAppointments { get; set; }
    }
}
