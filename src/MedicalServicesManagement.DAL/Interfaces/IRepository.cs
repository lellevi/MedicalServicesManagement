using MedicalServicesManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.DAL.Interfaces
{
    public interface IRepository <T>
        where T : IEntity
    {
        Task<string> CreateAsync(T item);
        Task DeleteByIdAsync(string id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(string id,
            params Expression<Func<T, object>>[] includes);
        Task UpdateAsync(T item);
    }
}
