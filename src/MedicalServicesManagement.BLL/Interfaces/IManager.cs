using MedicalServicesManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IManager<TDTO,TEntity>
        where TDTO : IDTO
        where TEntity : IEntity
    {
        Task CreateAsync(TDTO item);
        Task DeleteByIdAsync(string id);
        Task<IReadOnlyCollection<TDTO>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TDTO> GetByIdAsync(string id);
        Task UpdateAsync(TDTO item);
    }
}
