using AutoMapper;
using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using System;

namespace MedicalServicesManagement.BLL.Managers
{
    public class AppointmentServiceManager : BaseManager<AppointmentServiceDTO, AppointmentServiceDAL>, IAppointmentServiceManager
    {
        protected override string EntityName { get => "appointmentService"; }

        public AppointmentServiceManager(IRepository<AppointmentServiceDAL> repository, IMapper mapper) : base(repository, mapper) { }

        protected override void Validate(AppointmentServiceDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.AdditionalServiceId) || item.AdditionalServiceId.Length > 36)
                throw new ArgumentException("AdditionalServiceId is required and max length 36");
            if (string.IsNullOrWhiteSpace(item.AppointmentId) || item.AppointmentId.Length > 36)
                throw new ArgumentException("AppointmentId is required and max length 36");
            if (item.Amount < 1)
                throw new ArgumentException("Amount must be at least 1");
        }
    }
}
