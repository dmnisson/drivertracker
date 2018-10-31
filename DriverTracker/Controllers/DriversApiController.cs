using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DriverTracker.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    public class DriversApiController : Controller
    {
        private readonly MvcDriverContext _context;

        public DriversApiController(MvcDriverContext context)
        {
            _context = context;
        }

        // GET: api/driversapi
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            return _context.Drivers.ToList();
        }

        // GET api/driversapi/5
        [HttpGet("{id}")]
        public Driver Get(int id)
        {
            return _context.Drivers.FirstOrDefault(m => m.DriverID == id);
        }

        // POST api/driversapi/new
        [HttpPost("new")]
        public void Post(Driver driver)
        {
            _context.Add(driver);
            _context.SaveChanges();
        }

        // PUT api/driversapi/5
        [HttpPut("{id}")]
        public void Put(int id, Driver driver)
        {
            var existingDriver = _context.Drivers.Find(id);
            if (existingDriver == null) {
                return;
            }

            existingDriver.UserID = driver.UserID;
            existingDriver.Name = driver.Name;
            existingDriver.LicenseNumber = driver.LicenseNumber;

            _context.Drivers.Update(existingDriver);
            _context.SaveChanges();
        }

        // DELETE api/driversapi/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingDriver = _context.Drivers.Find(id);
            if (existingDriver == null)
            {
                return;
            }

            _context.Drivers.Remove(existingDriver);
            _context.SaveChanges();
        }
    }
}
