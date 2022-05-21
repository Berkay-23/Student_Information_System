using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        // Read Operations
        IQueryable<Lesson> GetAll(bool tracking = true);
        IQueryable<Lesson> GetWhere(Expression<Func<Lesson, bool>> method, bool tracking = true);
        Task<Lesson> GetSingleAsync(Expression<Func<Lesson, bool>> method, bool tracking = true);
        Task<Lesson> GetByIdAsync(short id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(Lesson entity);
        Task<bool> AddRangeAsync(List<Lesson> entities);
        bool Remove(Lesson entity);
        bool RemoveRange(List<Lesson> entities);
        Task<bool> RemoveAsync(int id);
        bool Update(Lesson entity);
        Task<int> SaveAsync();
    }
}
