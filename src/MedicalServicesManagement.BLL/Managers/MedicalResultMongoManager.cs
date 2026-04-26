using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;

namespace MedicalServicesManagement.BLL.Managers
{
    public class MedicalResultMongoManager : MongoBaseManager<MedicalResult, MedicalResultDto>
    {
        public MedicalResultMongoManager(
            IMongoRepository<MedicalResult> repository,
            IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
