using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface INoteRepository : IRepository<Note>
    {
        // Read Operations
        IQueryable<Note> GetAll(bool tracking = true);
        IQueryable<Note> GetWhere(Expression<Func<Note, bool>> method, bool tracking = true);
        Task<Note> GetSingleAsync(Expression<Func<Note, bool>> method, bool tracking = true);
        Task<Note> GetByIdAsync(string id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(Note entity);
        Task<bool> AddRangeAsync(List<Note> entities);
        bool Remove(Note entity);
        bool RemoveRange(List<Note> entities);
        Task<bool> RemoveAsync(string id);
        bool Update(Note entity);
        Task<int> SaveAsync();
    }
}
