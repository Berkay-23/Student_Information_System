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
    public class StudentRepository : IStudentRepository
    {
        private readonly SISContext _context;

        public StudentRepository(SISContext context)
        {
            _context = context;
        }

        public DbSet<Student> Table
             => _context.Set<Student>();

        

        public IQueryable<Student> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<Student> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(student => student.StudentNo == id);
        }

        public async Task<Student> GetSingleAsync(Expression<Func<Student, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<Student> GetWhere(Expression<Func<Student, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<bool> AddAsync(Student entity)
        {
            EntityEntry<Student> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<Student> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(Student entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            Student entity = await Table.FirstOrDefaultAsync(student => student.StudentNo == id);
            return Remove(entity);
        }

        public bool RemoveRange(List<Student> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public bool Update(Student entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
