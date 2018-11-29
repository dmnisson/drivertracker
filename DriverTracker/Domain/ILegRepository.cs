using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public interface ILegRepository
    {
        Task<IEnumerable<Leg>> ListAsync();
        Task<IEnumerable<Leg>> ListAsync(Expression<Func<Leg, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<Leg, bool>> predicate);
        Task<Leg> Get(int id);

        Task<IEnumerable<Leg>> ListForDriverAsync(int id);
        Task<IEnumerable<Leg>> ListForDriverAsync(int id, Expression<Func<Leg, bool>> predicate);
        Task<int> CountDriverLegsAsync(int id);
        Task<int> CountDriverLegsAsync(int id, Expression<Func<Leg, bool>> predicate);

        Task AddAsync(Leg leg);
        Task DeleteAsync(Leg leg);
        Task EditAsync(Leg leg);
    }
}
