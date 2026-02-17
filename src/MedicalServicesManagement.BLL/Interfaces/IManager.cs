using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IManager<T>
        where T : IDTO
    {
        Task CreateAsync(T item);
        Task DeleteByIdAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T item);
    }
}
