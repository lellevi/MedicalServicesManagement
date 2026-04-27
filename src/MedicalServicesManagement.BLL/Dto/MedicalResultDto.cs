using MedicalServicesManagement.BLL.Interfaces;
using System;

namespace MedicalServicesManagement.BLL.Dto
{
    public class MedicalResultDto : BaseDTO, IMongoDto
    {
        public AppointmentDTO Appointment { get; set; }

        public EntityUserDTO Medic { get; set; }

        public EntityUserDTO Patient { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ExaminationData { get; set; }

        public string Diagnosis { get; set; }

        public string Recommendations { get; set; }
    }
}
