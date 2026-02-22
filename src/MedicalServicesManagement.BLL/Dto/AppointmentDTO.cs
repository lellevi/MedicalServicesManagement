using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using System;

namespace MedicalServicesManagement.BLL.Dto
{
    public class AppointmentDTO : IDTO
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string ServiceId { get; set; }
        public string MedicId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public decimal TotalCost { get; set; }
    }
}
