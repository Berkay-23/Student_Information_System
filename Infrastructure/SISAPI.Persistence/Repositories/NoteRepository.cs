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
    public class NoteRepository : INoteRepository
    {
        private readonly SISContext _context;

        public NoteRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<Note> Table
             => _context.Set<Note>();


        public IQueryable<Note> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Note> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.LessonId == Int32.Parse(id));
        }

        public async Task<Note> GetSingleAsync(Expression<Func<Note, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<Note> GetWhere(Expression<Func<Note, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(Note entity)
        {
            EntityEntry<Note> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<Note> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(Note entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            Note entity = await Table.FirstOrDefaultAsync(entity => entity.LessonId == Int32.Parse(id));
            return Remove(entity);
        }

        public bool RemoveRange(List<Note> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(Note entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

    }
}
