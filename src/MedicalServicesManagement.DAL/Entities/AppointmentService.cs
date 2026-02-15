namespace MedicalServicesManagement.DAL.Entities
{
    public class AppointmentService : BaseEntity
    {
        public string AdditionalServiceId { get; set; }
        public string AppointmentId { get; set; }
        public int Amount { get; set; }
    }
}
