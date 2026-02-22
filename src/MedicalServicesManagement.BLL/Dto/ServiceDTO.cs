using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class ServiceDTO : IDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool ForAdults { get; set; }
        public string MedSpecialityId { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
    }
}
