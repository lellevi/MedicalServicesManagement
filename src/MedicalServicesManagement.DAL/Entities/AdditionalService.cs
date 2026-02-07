namespace MedicalServicesManagement.DAL.Entities
{
    public class AdditionalService : BaseEntity
    {
        public string Name { get; set; }
        public int MedSpecialityId { get; set; }
        public decimal Price { get; set; }
    }
}
