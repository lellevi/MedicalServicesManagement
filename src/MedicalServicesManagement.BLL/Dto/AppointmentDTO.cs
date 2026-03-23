using System;

namespace MedicalServicesManagement.BLL.Dto
{
    public class AppointmentDTO : BaseDTO
    {
        public string PatientId { get; set; }
        public EntityUserDTO Patient { get; set; }
        public string ServiceId { get; set; }
        public ServiceDTO Service { get; set; }

        public string MedicId { get; set; }

        public EntityUserDTO Medic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enums.AppointmentStatus Status { get; set; }
        public decimal TotalCost { get; set; }
    }
}
