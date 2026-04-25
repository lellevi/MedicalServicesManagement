using MedicalServicesManagement.DAL.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicalServicesManagement.DAL.Entities
{
    public abstract class BaseMongoEntity : IEntity
    {
        [BsonId]
        public required string Id { get; set; }
    }
}
