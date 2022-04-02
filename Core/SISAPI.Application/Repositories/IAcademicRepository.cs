using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface IAcademicRepository : IRepository<Academic>
    {
        // Read Operations
        IQueryable<Academic> GetAll(bool tracking = true);
        IQueryable<Academic> GetWhere(Expression<Func<Academic, bool>> method, bool tracking = true);
        Task<Academic> GetSingleAsync(Expression<Func<Academic, bool>> method, bool tracking = true);
        Task<Academic> GetByIdAsync(string id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(Academic entity);
        Task<bool> AddRangeAsync(List<Academic> entities);
        bool Remove(Academic entity);
        bool RemoveRange(List<Academic> entities);
        Task<bool> RemoveAsync(string id);
        bool Update(Academic entity);
        Task<int> SaveAsync();
    }
}
