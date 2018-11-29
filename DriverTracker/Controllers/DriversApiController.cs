using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using DriverTracker.Models;
using DriverTracker.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Authorize(Roles = "Admin, Analyst, Driver")]
    [Route("api/[controller]")]
    public class DriversApiController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILegRepository _legRepository;

        public DriversApiController(IDriverRepository driverRepository, ILegRepository legRepository)
        {
            _driverRepository = driverRepository;
            _legRepository = legRepository;
        }

        // GET: api/driversapi
        [HttpGet]
        public async Task<IEnumerable<Driver>> Get()
        {
            return await _driverRepository.ListAsync();
        }

        // GET api/driversapi/5
        [HttpGet("{id}", Name = "GetDriver")]
        public async Task<Driver> Get(int id)
        {
            return await _driverRepository.GetAsync(id);
        }

        // POST api/driversapi/new
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] Driver driver)
        {
            await _driverRepository.AddAsync(driver);

            return CreatedAtRoute("GetDriver", new { id = driver.DriverID }, driver);
        }

        // PUT api/driversapi/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] Driver driver)
        {
            var existingDriver = await _driverRepository.GetAsync(id);
            if (existingDriver == null) {
                return;
            }

            existingDriver.UserID = driver.UserID;
            existingDriver.Name = driver.Name;
            existingDriver.LicenseNumber = driver.LicenseNumber;

            await _driverRepository.EditAsync(driver);
        }

        // DELETE api/driversapi/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var existingDriver = await _driverRepository.GetAsync(id);
            if (existingDriver == null)
            {
                return;
            }

            await _driverRepository.DeleteAsync(existingDriver);
        }
    }
}
