using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using DriverTracker.Models;
using DriverTracker.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DriverTracker.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DriversApiController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IAuthorizationService _authorizationService;

        public DriversApiController(IDriverRepository driverRepository, 
            IAuthorizationService authorizationService)
        {
            _driverRepository = driverRepository;
            _authorizationService = authorizationService;
        }

        // GET: api/driversapi
        [HttpGet]
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IEnumerable<Driver>> Get()
        {
            return await _driverRepository.ListAsync();
        }

        // GET api/driversapi/5
        [HttpGet("{id}", Name = "GetDriver")]
        public async Task<IActionResult> Get(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var driver = await _driverRepository.GetAsync(id);
            var authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(driver);
        }

        // POST api/driversapi/new
        [HttpPost("new")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Driver driver)
        {
            await _driverRepository.AddAsync(driver);

            return CreatedAtRoute("GetDriver", new { id = driver.DriverID }, driver);
        }

        // PUT api/driversapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Driver driver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var existingDriver = await _driverRepository.GetAsync(id);
            if (existingDriver == null) {
                return NotFound();
            }



            var authResult = await _authorizationService.AuthorizeAsync(User, existingDriver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            existingDriver.Name = driver.Name;
            existingDriver.LicenseNumber = driver.LicenseNumber;

            await _driverRepository.EditAsync(existingDriver);

            return Ok();
        }

        // DELETE api/driversapi/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
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
