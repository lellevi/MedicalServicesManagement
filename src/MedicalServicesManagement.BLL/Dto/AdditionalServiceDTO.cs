namespace MedicalServicesManagement.BLL.Dto
{
    public class AdditionalServiceDTO : BaseDTO
    {
        public string Name { get; set; }
        public string MedSpecialityId { get; set; }
        public decimal Price { get; set; }
    }
}
