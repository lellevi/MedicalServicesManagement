namespace MedicalServicesManagement.WebApp.Models
{
    public class TakeConfirmViewModel
    {
        public AppointmentViewModel Appointment { get; set; }

        public UserViewModel Patient { get; set; }

        public string AppointmentId { get; set; }

        public string PatientId { get; set; }
    }
}
