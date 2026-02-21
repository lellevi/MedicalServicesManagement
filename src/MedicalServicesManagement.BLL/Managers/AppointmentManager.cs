using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Managers
{
    public class AppointmentManager : BaseManager<AppointmentDTO, Appointment>, IAppointmentManager
    {
        protected override string EntityName { get => "appointment"; }

        public AppointmentManager(IRepository<Appointment> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(AppointmentDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.PatientId) || item.PatientId.Length > 36)
                throw new ArgumentException("PatientId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.ServiceId) || item.ServiceId.Length > 36)
                throw new ArgumentException("ServiceId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.MedicId) || item.MedicId.Length > 36)
                throw new ArgumentException("MedicId is required and max length 36");
            if (item.StartDate >= item.EndDate)
                throw new ArgumentException("StartDate must be before EndDate");
            if (item.TotalCost < 0)
                throw new ArgumentException("TotalCost must be zero or positive");
        }
    }
}
