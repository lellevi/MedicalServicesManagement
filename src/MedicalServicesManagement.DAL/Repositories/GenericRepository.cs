using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalServicesManagement.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly MedServiceContext _context;

        public GenericRepository(MedServiceContext context)
        {
            _context = context;
        }

        public async Task<string> CreateAsync(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
            item.Id ??= Guid.NewGuid().ToString();

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            _context.Entry(item).State = EntityState.Detached;

            return item.Id;
        }

        public async Task DeleteByIdAsync(string id)
        {
            var item = await GetSingleAsync(x => x.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException("Entity not found.");
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] includes = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var items = await query.ToListAsync();
            return items;
        }

        public async Task<T> GetSingleAsync(
           Expression<Func<T, bool>> filter,
           Expression<Func<T, object>>[] includes = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var item = await query.Where(filter).SingleOrDefaultAsync();
            if (item == null)
            {
                _context.Entry(item).State = EntityState.Detached;
            }

            return item;
        }

        public async Task UpdateAsync(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (item.Id == null)
            {
                return;
            }

            var query = _context.Set<T>().AsNoTracking();

            var entityExists = await query.AnyAsync(x => x.Id == item.Id);

            if (!entityExists)
            {
                throw new KeyNotFoundException("Entity not found.");
            }

            _context.Update(item);
            await _context.SaveChangesAsync();

            _context.Entry(item).State = EntityState.Detached;
        }
    }
}
