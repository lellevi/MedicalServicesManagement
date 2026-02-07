namespace MedicalServicesManagement.DAL.Entities
{
    public class MedicalResult : BaseEntity
    {
        public int AppointmentId { get; set; }
        public string MedicId { get; set; }
        public string PatientId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ExaminationData { get; set; }
        public string Diagnosis { get; set; }
        public string Recommendations { get; set; }
    }
}
