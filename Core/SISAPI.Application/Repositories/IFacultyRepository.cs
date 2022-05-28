using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SISAPI.Domain.Entities;
using System.Collections.Generic;

namespace SISAPI.Application.Repositories
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        // Read Operations
        IQueryable<Faculty> GetAll(bool tracking = true);
        IQueryable<Faculty> GetWhere(Expression<Func<Faculty, bool>> method, bool tracking = true);
        Task<Faculty> GetSingleAsync(Expression<Func<Faculty, bool>> method, bool tracking = true);
        Task<Faculty> GetByIdAsync(short id, bool tracking = true);

        
        // Write Operations
        Task<bool> AddAsync(Faculty entity);
        Task<bool> AddRangeAsync(List<Faculty> entities);
        bool Remove(Faculty entity);
        bool RemoveRange(List<Faculty> entities);
        Task<bool> RemoveAsync(short id);
        bool Update(Faculty entity);
        Task<int> SaveAsync();
    }
}
