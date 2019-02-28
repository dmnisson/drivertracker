using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DriverTracker.Domain;
using DriverTracker.Models;

using Newtonsoft.Json;

namespace DriverTracker.Tests
{
    public class MockDriverRepository : IDriverRepository
    {

        private Driver[] _drivers;

        public MockDriverRepository(Driver[] drivers)
        {
            _drivers = drivers;
        }

        public async Task AddAsync(Driver driver)
        {
            Console.WriteLine("AddAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(driver)));
            Console.WriteLine();
        }

        public async Task<int> CountAsync()
        {
            Console.WriteLine("CountAsync called");
            Console.WriteLine();
            return await Task.Run(() => _drivers.Length);
        }

        public async Task<int> CountAsync(Expression<Func<Driver, bool>> predicate)
        {
            Console.WriteLine("CountAsync called");
            Console.Write(predicate);
            Console.WriteLine();
            return await Task.Run(() => _drivers.AsQueryable().Where(predicate).Count());
        }

        public async Task DeleteAsync(Driver driver)
        {
            Console.WriteLine("DeleteAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(driver)));
            Console.WriteLine();
        }

        public bool DriverExists(int id)
        {
            Console.WriteLine("DriverExists called");
            return _drivers.Any(driver => driver.DriverID == id);
        }

        public async Task EditAsync(Driver driver)
        {
            Console.WriteLine("EditAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(driver)));
            Console.WriteLine();
        }

        public async Task<Driver> GetAsync(int id)
        {
            Console.WriteLine("GetAsync called");
            return await Task.Run(() => _drivers.FirstOrDefault(driver => driver.DriverID == id));
        }

        public async Task<IEnumerable<Driver>> ListAsync()
        {
            Console.WriteLine("ListAsync called");
            return await Task.Run(() => _drivers.AsEnumerable());
        }

        public async Task<IEnumerable<Driver>> ListAsync(Expression<Func<Driver, bool>> predicate)
        {
            Console.WriteLine("ListAsync called");
            return await Task.Run(() => _drivers.AsQueryable().Where(predicate));
        }
    }
}
