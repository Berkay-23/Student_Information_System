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
    public class LessonRepository : ILessonRepository
    {
        private readonly SISContext _context;

        public LessonRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<Lesson> Table
             => _context.Set<Lesson>();


        public IQueryable<Lesson> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Lesson> GetByIdAsync(short id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.LessonId == id);
        }

        public async Task<Lesson> GetSingleAsync(Expression<Func<Lesson, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<Lesson> GetWhere(Expression<Func<Lesson, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(Lesson entity)
        {
            EntityEntry<Lesson> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<Lesson> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(Lesson entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Lesson entity = await Table.FirstOrDefaultAsync(entity => entity.LessonId == id);
            return Remove(entity);
        }

        public bool RemoveRange(List<Lesson> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(Lesson entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
