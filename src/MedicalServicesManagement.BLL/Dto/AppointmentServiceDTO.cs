using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class AppointmentServiceDTO : IDTO
    {
        public string Id { get; set; }
        public string AdditionalServiceId { get; set; }
        public string AppointmentId { get; set; }
        public int Amount { get; set; }
    }
}
