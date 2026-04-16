using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalServicesManagement.DAL.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedServiceContext context)
            : base(context)
        {
        }

        public async Task<List<Appointment>> GetAllFreeAppointmentsOrderedAsync(
            Expression<Func<Appointment, bool>> filter,
            Expression<Func<Appointment, object>>[] includes = null)
        {
            var query = _context.Set<Appointment>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.OrderBy(a => a.StartDate.Date)
                        .ThenBy(a => a.StartDate.TimeOfDay);

            return await query.ToListAsync();
        }
    }
}
