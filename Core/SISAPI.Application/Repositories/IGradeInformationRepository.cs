using SISAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Application.Repositories
{
    public interface IGradeInformationRepository : IRepository<GradeInformation>
    {
        // Read Operations
        IQueryable<GradeInformation> GetAll(bool tracking = true);
        IQueryable<GradeInformation> GetWhere(Expression<Func<GradeInformation, bool>> method, bool tracking = true);
        Task<GradeInformation> GetSingleAsync(Expression<Func<GradeInformation, bool>> method, bool tracking = true);
        Task<GradeInformation> GetByIdAsync(string id, bool tracking = true);


        // Write Operations

        Task<bool> AddAsync(GradeInformation entity);
        Task<bool> AddRangeAsync(List<GradeInformation> entities);
        bool Remove(GradeInformation entity);
        bool RemoveRange(List<GradeInformation> entities);
        Task<bool> RemoveAsync(string id);
        bool Update(GradeInformation entity);
        Task<int> SaveAsync();
    }
}
