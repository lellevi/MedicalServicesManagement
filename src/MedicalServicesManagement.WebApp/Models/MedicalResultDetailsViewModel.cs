using System.Collections.Generic;

namespace MedicalServicesManagement.WebApp.Models
{
    public class MedicalResultDetailsViewModel
    {
        public AppointmentViewModel Appointment { get; set; }

        public UserViewModel Patient { get; set; }

        public UserViewModel Medic { get; set; }

        public ServiceViewModel Service { get; set; }

        public List<AdditionalServiceViewModel> AdditionalServices { get; set; }

        public List<AppointmentServiceViewModel> AppointmentServices { get; set; }

        public MedicalResultViewModel MedicalResult { get; set; }

        public decimal TotalCost { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
