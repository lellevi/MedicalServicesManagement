using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class AdditionalServiceDTO : IDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MedSpecialityId { get; set; }
        public decimal Price { get; set; }
    }
}
