using Microsoft.EntityFrameworkCore;
using SISAPI.Domain.Entities.Common;

namespace SISAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
