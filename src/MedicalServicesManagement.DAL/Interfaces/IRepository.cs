using MedicalServicesManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.DAL.Interfaces
{
    public interface IRepository <T>
        where T : BaseEntity
    {
        Task<string> CreateAsync(T item);
        Task DeleteByIdAsync(string id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T item);
    }
}
