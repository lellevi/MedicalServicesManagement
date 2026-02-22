using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class EntityUserDTO : IDTO
    {
        public string Id { get; set; }
        public string AuthUserId { get; set; }
        public string MedSpecialityId { get; set; }
        public MedSpecialityDTO MedSpeciality { get; set; }
        public string MedInfo { get; set; }
    }
}
