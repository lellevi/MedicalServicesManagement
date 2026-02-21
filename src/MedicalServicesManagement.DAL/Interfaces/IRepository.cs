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
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T item);
    }
}
