namespace MedicalServicesManagement.BLL.Dto
{
    public class ServiceDTO : BaseDTO
    {
        public string Name { get; set; }
        public bool ForAdults { get; set; }
        public string MedSpecialityId { get; set; }
        public MedSpecialityDTO MedSpeciality { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
    }
}
