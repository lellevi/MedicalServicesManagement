namespace MedicalServicesManagement.DAL.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public bool ForAdults { get; set; }
        public int MedSpecialityId { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
    }
}
