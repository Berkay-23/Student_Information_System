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
    public class GradeInformationRepository : IGradeInformationRepository
    {
        private readonly SISContext _context;

        public GradeInformationRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<GradeInformation> Table
             => _context.Set<GradeInformation>();


        public IQueryable<GradeInformation> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<GradeInformation> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.StudentNo == id);
        }

        public async Task<GradeInformation> GetSingleAsync(Expression<Func<GradeInformation, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<GradeInformation> GetWhere(Expression<Func<GradeInformation, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(GradeInformation entity)
        {
            EntityEntry<GradeInformation> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<GradeInformation> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(GradeInformation entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            GradeInformation entity = await Table.FirstOrDefaultAsync(entity => entity.StudentNo == id);
            return Remove(entity);
        }

        public bool RemoveRange(List<GradeInformation> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(GradeInformation entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
