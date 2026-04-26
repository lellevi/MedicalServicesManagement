using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using System;

namespace MedicalServicesManagement.BLL.Dto
{
    public class MedicalResultDto : BaseDTO, IMongoDto
    {
        public Appointment Appointment { get; set; }

        public EntityUser Medic { get; set; }

        public EntityUser Patient { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ExaminationData { get; set; }

        public string Diagnosis { get; set; }

        public string Recommendations { get; set; }
    }
}
