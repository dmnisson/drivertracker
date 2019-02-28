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
    public class MockLegRepository : ILegRepository
    {

        private readonly Leg[] _legs;

        public MockLegRepository(Leg[] legs)
        {
            _legs = legs;
        }

        public async Task AddAsync(Leg leg)
        {
            Console.WriteLine("AddAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(leg)));
            Console.WriteLine();
        }

        public async Task<int> CountAsync()
        {
            Console.WriteLine("CountAsync called");
            Console.WriteLine();
            return await Task.Run(() => _legs.Length);
        }

        public async Task<int> CountAsync(Expression<Func<Leg, bool>> predicate)
        {
            Console.WriteLine("CountAsync called");
            Console.Write(predicate);
            Console.WriteLine();
            return await Task.Run(() => _legs.AsQueryable().Where(predicate).Count());
        }

        public async Task<int> CountDriverLegsAsync(int id)
        {
            Console.WriteLine("CountDriverLegsAsync called");
            Console.WriteLine("id: " + id);
            return await Task.Run(() => _legs.AsQueryable().Where(leg => leg.DriverID == id).Count());
        }

        public async Task<int> CountDriverLegsAsync(int id, Expression<Func<Leg, bool>> predicate)
        {
            Console.WriteLine("CountDriverLegsAsync called");
            Console.WriteLine("id: " + id);
            Console.Write(predicate);
            Console.WriteLine();
            return await Task.Run(() => _legs.AsQueryable().Where(leg => leg.DriverID == id).Count());
        }

        public async Task DeleteAsync(Leg leg)
        {
            Console.WriteLine("DeleteAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(leg)));
            Console.WriteLine();
        }

        public async Task EditAsync(Leg leg)
        {
            Console.WriteLine("EditAsync called");
            Console.Write(await Task.Run(() => JsonConvert.SerializeObject(leg)));
            Console.WriteLine();
        }

        public async Task<Leg> Get(int id)
        {
            Console.WriteLine("Get called");
            Console.WriteLine("id: " + id);
            return await Task.Run(() => _legs.FirstOrDefault(leg => leg.LegID == id));
        }

        public async Task<IEnumerable<Leg>> ListAsync()
        {
            Console.WriteLine("ListAsync called");
            return await Task.Run(() => _legs.AsEnumerable());
        }

        public async Task<IEnumerable<Leg>> ListAsync(Expression<Func<Leg, bool>> predicate)
        {
            Console.WriteLine("ListAsync called");
            Console.Write(predicate);
            Console.WriteLine();
            return await Task.Run(() => _legs.AsEnumerable());
        }

        public async Task<IEnumerable<Leg>> ListForDriverAsync(int id)
        {
            Console.WriteLine("ListForDriverAsync called");
            Console.WriteLine("id: " + id);
            Console.WriteLine();
            return await Task.Run(() => _legs.AsQueryable().Where(leg => leg.DriverID == id));
        }

        public async Task<IEnumerable<Leg>> ListForDriverAsync(int id, Expression<Func<Leg, bool>> predicate)
        {
            Console.WriteLine("ListForDriverAsync called");
            Console.WriteLine("id: " + id);
            Console.Write(predicate);
            Console.WriteLine();
            return await Task.Run(() => _legs.AsQueryable().Where(leg => leg.DriverID == id));
        }
    }
}
