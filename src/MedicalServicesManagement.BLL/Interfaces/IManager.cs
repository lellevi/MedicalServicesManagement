using MedicalServicesManagement.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IManager<TDTO,TEntity>
        where TDTO : IDTO
        where TEntity : IEntity
    {
        Task CreateAsync(TDTO item);
        Task DeleteByIdAsync(string id);
        Task<List<TDTO>> GetAllAsync();
        Task<TDTO> GetByIdAsync(string id);
        Task UpdateAsync(TDTO item);
    }
}
