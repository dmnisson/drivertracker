using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using DriverTracker.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Authorize(Roles = "Admin, Analyst, Driver")]
    [Route("api/[controller]")]
    public class DriversApiController : ControllerBase
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
        [HttpGet("{id}", Name = "GetDriver")]
        public Driver Get(int id)
        {
            return _context.Drivers.FirstOrDefault(m => m.DriverID == id);
        }

        // POST api/driversapi/new
        [HttpPost("new")]
        public IActionResult Post([FromBody] Driver driver)
        {
            _context.Add(driver);
            _context.SaveChanges();

            return CreatedAtRoute("GetDriver", new { id = driver.DriverID }, driver);
        }

        // PUT api/driversapi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Driver driver)
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
        [Authorize(Roles = "Admin")]
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
