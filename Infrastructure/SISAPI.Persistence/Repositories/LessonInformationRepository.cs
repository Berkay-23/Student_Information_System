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
    public class LessonInformationRepository: ILessonInformationRepository
    {
        private readonly SISContext _context;

        public LessonInformationRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<LessonInformation> Table
             => _context.Set<LessonInformation>();


        public IQueryable<LessonInformation> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<LessonInformation> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.StudentNo == id);
        }

        public async Task<LessonInformation> GetSingleAsync(Expression<Func<LessonInformation, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<LessonInformation> GetWhere(Expression<Func<LessonInformation, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(LessonInformation entity)
        {
            EntityEntry<LessonInformation> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<LessonInformation> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(LessonInformation entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            LessonInformation entity = await Table.FirstOrDefaultAsync(entity => entity.StudentNo == id);
            return Remove(entity);
        }

        public bool RemoveRange(List<LessonInformation> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(LessonInformation entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
