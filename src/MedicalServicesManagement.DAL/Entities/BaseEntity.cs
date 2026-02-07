using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.DAL.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
