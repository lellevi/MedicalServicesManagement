using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly MedServiceContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(MedServiceContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<string> CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task DeleteByIdAsync(string id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Entity not found.");

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAllAsync() => _dbSet.AsQueryable().AsNoTracking();

        public async Task<T> GetByIdAsync(string id) => await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T item)
        {
            var existing = _dbSet.Find(item.Id);
            if (existing == null)
                throw new KeyNotFoundException("Entity not found.");

            _context.Entry(existing).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }
    }
}
