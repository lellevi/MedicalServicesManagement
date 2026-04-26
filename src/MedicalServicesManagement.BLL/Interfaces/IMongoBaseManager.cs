using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedicalServicesManagement.BLL.Interfaces
{
    public interface IMongoBaseManager<TEntity, TDto>
    {
        Task CreateAsync(TEntity entity);

        Task CreateAsync(TDto dto);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TDto>> GetAllDtosAsync();

        Task<TEntity> GetByIdAsync(string id);

        Task UpdateAsync(string id, TEntity entity);

        Task DeleteAsync(string id);

        public Task UpdateAsync<TField>(string id, Expression<Func<TEntity, TField>> field, TField newValue);

        public Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression);

        public Task<List<TEntity>> FilterAsync<TField>(Expression<Func<TEntity, TField>> field, IEnumerable<TField> values);
    }
}
