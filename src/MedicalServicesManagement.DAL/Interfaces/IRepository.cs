using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.DAL.Interfaces
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<string> CreateAsync(T item);

        Task DeleteByIdAsync(string id);

        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);

        Task UpdateAsync(T item);
    }
}
