using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DriverTracker.Models;

namespace DriverTracker.Repositories
{
    public class LegRepository : ILegRepository
    {
        private readonly MvcDriverContext _context;

        public LegRepository(MvcDriverContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Leg leg)
        {
            _context.Add(leg);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Legs.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<Leg, bool>> predicate)
        {
            return await _context.Legs.Where(predicate).CountAsync();
        }

        public async Task<int> CountDriverLegsAsync(int id)
        {
            return await _context.Legs.Where(m => m.DriverID == id).CountAsync();
        }

        public async Task<int> CountDriverLegsAsync(int id, Expression<Func<Leg, bool>> predicate)
        {
            return await _context.Legs.Where(m => m.DriverID == id).CountAsync();
        }

        public async Task DeleteAsync(Leg leg)
        {
            _context.Legs.Remove(leg);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Leg leg)
        {
            _context.Update(leg);
            await _context.SaveChangesAsync();
        }

        public async Task<Leg> Get(int id)
        {
            return await _context.Legs.FirstOrDefaultAsync(m => m.LegID == id);
        }

        public async Task<IEnumerable<Leg>> ListAsync()
        {
            return await _context.Legs.ToListAsync();
        }

        public async Task<IEnumerable<Leg>> ListAsync(Expression<Func<Leg, bool>> predicate)
        {
            return await _context.Legs.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Leg>> ListForDriverAsync(int id)
        {
            return await _context.Legs.Where(m => m.DriverID == id).ToListAsync();
        }

        public async Task<IEnumerable<Leg>> ListForDriverAsync(int id, Expression<Func<Leg, bool>> predicate)
        {
            return await _context.Legs.Where(m => m.DriverID == id)
                                 .Where(predicate).ToListAsync();
        }
    }
}
