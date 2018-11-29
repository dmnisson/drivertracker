using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq.Expressions;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> ListAsync();
        Task<IEnumerable<Driver>> ListAsync(Expression<Func<Driver, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<Driver, bool>> predicate);
        Task<Driver> GetAsync(int id);
        bool DriverExists(int id);
        Task AddAsync(Driver driver);
        Task DeleteAsync(Driver driver);
        Task EditAsync(Driver driver);
    }
}
