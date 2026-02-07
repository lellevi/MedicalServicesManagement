namespace MedicalServicesManagement.DAL.Entities
{
    public class AppointmentService : BaseEntity
    {
        public int AdditionalServiceId { get; set; }
        public int AppointmentId { get; set; }
        public int Amount { get; set; }
    }
}
