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
            Expression<Func<T, object>>[] includes = null);

        Task<T> GetSingleAsync(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>>[] includes = null);

        Task UpdateAsync(T item);
    }
}
