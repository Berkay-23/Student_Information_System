using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using SISAPI.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SISAPI.Persistence.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly SISContext _context;

        public FacultyRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<Faculty> Table => _context.Set<Faculty>();

        public IQueryable<Faculty> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Faculty> GetByIdAsync(short id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.FacultyId == id);
        }

        public async Task<Faculty> GetSingleAsync(Expression<Func<Faculty, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<Faculty> GetWhere(Expression<Func<Faculty, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(Faculty entity)
        {
            EntityEntry<Faculty> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<Faculty> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(Faculty entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(short id)
        {
            Faculty entity = await Table.FirstOrDefaultAsync(entity => entity.FacultyId == id);
            return Remove(entity);
        }

        public bool RemoveRange(List<Faculty> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(Faculty entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
