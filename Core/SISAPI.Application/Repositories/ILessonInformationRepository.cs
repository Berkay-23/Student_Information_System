using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface ILessonInformationRepository
    {
        // Read Operations
        IQueryable<LessonInformation> GetAll(bool tracking = true);
        IQueryable<LessonInformation> GetWhere(Expression<Func<LessonInformation, bool>> method, bool tracking = true);
        Task<LessonInformation> GetSingleAsync(Expression<Func<LessonInformation, bool>> method, bool tracking = true);
        Task<LessonInformation> GetByIdAsync(string id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(LessonInformation entity);
        Task<bool> AddRangeAsync(List<LessonInformation> entities);
        bool Remove(LessonInformation entity);
        bool RemoveRange(List<LessonInformation> entities);
        Task<bool> RemoveAsync(string id);
        bool Update(LessonInformation entity);
        Task<int> SaveAsync();
    }
}
