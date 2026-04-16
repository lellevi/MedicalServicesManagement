using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Entities;

namespace MedicalServicesManagement.DAL.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<List<Appointment>> GetAllFreeAppointmentsOrderedAsync(
            Expression<Func<Appointment, bool>> filter,
            Expression<Func<Appointment, object>>[] includes = null);
    }
}
