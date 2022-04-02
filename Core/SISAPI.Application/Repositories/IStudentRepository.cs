using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        // Read Operations
        IQueryable<Student> GetAll(bool tracking = true);
        IQueryable<Student> GetWhere(Expression<Func<Student, bool>> method, bool tracking = true);
        Task<Student> GetSingleAsync(Expression<Func<Student, bool>> method, bool tracking = true);
        Task<Student> GetByIdAsync(string id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(Student entity);
        Task<bool> AddRangeAsync(List<Student> entities);
        bool Remove(Student entity);
        bool RemoveRange(List<Student> entities);
        Task<bool> RemoveAsync(string id);
        bool Update(Student entity);
        Task<int> SaveAsync();
    }
}
