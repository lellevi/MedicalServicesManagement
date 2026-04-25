using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Factories;

namespace MedicalServicesManagement.DAL.Repositories
{
    public class MedicalResultRepository(IMongoDbFactory mongoDbFactory)
        : MongoRepository<MedicalResult>(mongoDbFactory)
    {
        public override string CollectionName => "MedicalResults";
    }
}
