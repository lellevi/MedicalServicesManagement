using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class BaseMongoDto : IMongoDto
    {
        public required string Id { get; set; }
    }
}
