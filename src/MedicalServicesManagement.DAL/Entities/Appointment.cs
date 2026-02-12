using System;

namespace MedicalServicesManagement.DAL.Entities
{
    public class Appointment : BaseEntity
    {
        public string PatientId { get; set; }
        public string ServiceId { get; set; }
        public string MedicId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public decimal TotalCost { get; set; }
    }
}
