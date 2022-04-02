using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SISAPI.Application.Repositories;
using SISAPI.Domain.Entities;
using SISAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Persistence.Repositories
{
    public class AcademicRepository : IAcademicRepository
    {
        private readonly SISContext _context;

        public AcademicRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<Academic> Table
             => _context.Set<Academic>();


        public IQueryable<Academic> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Academic> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.AcademicianId == Int32.Parse(id));
        }

        public async Task<Academic> GetSingleAsync(Expression<Func<Academic, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<Academic> GetWhere(Expression<Func<Academic, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(Academic entity)
        {
            EntityEntry<Academic> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<Academic> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(Academic entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            Academic entity = await Table.FirstOrDefaultAsync(entity => entity.AcademicianId == Int32.Parse(id));
            return Remove(entity);
        }

        public bool RemoveRange(List<Academic> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(Academic entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
