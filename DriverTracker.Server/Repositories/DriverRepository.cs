using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using DriverTracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DriverTracker.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly MvcDriverContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DriverRepository(MvcDriverContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddAsync(Driver driver)
        {
            _context.Add(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Drivers.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<Driver, bool>> predicate)
        {
            return await _context.Drivers.Where(predicate).CountAsync();
        }

        public async Task DeleteAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }

        public bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.DriverID == id);
        }

        public async Task EditAsync(Driver driver)
        {
            _context.Update(driver);
            await _context.SaveChangesAsync();
        }

        public Driver GetDriverModel(ClaimsPrincipal user)
        {
            return _context.Drivers.FirstOrDefault(m => m.UserIDString == _userManager.GetUserId(user));
        }

        public async Task<Driver> GetAsync(int id)
        {
            return await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == id);
        }

        public bool IsDriver(ClaimsPrincipal user)
        {
            return user.IsInRole("Driver") && _context.Drivers.Any(m => m.UserIDString == _userManager.GetUserId(user));
        }

        public async Task<IEnumerable<Driver>> ListAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<IEnumerable<Driver>> ListAsync(Expression<Func<Driver, bool>> predicate)
        {
            return await _context.Drivers.Where(predicate).ToListAsync();
        }
    }
}
