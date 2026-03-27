namespace MedicalServicesManagement.BLL.Dto
{
    public class AppointmentServiceDTO : BaseDTO
    {
        public string AdditionalServiceId { get; set; }

        public AdditionalServiceDTO AdditionalService { get; set; }

        public string AppointmentId { get; set; }

        public AppointmentDTO Appointment { get; set; }

        public int Amount { get; set; }
    }
}
