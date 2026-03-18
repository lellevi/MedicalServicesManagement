namespace MedicalServicesManagement.BLL.Dto
{
    public class AppointmentServiceDTO : BaseDTO
    {
        public string AdditionalServiceId { get; set; }
        public string AppointmentId { get; set; }
        public int Amount { get; set; }
    }
}
