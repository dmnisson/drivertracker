using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using DriverTracker.Domain;
using DriverTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LegsApiController : ControllerBase
    {
        private readonly ILegRepository _legRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IAuthorizationService _authorizationService;

        public LegsApiController(ILegRepository legRepository,
            IDriverRepository driverRepository,
            IAuthorizationService authorizationService)
        {
            _legRepository = legRepository;
            _driverRepository = driverRepository;
            _authorizationService = authorizationService;
        }

        // GET: api/legsapi
        [HttpGet]
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IEnumerable<Leg>> Get()
        {
            return await _legRepository.ListAsync();
        }

        // GET api/legsapi/fordriver/5
        [HttpGet("fordriver/{id}")]
        public async Task<IActionResult> GetForDriver(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Driver driver = await _driverRepository.GetAsync(id);
            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            IEnumerable<Leg> legs = await _legRepository.ListForDriverAsync(id);
            foreach (Leg leg in legs)
            {
                leg.Driver = null; // to prevent self-referencing loops during serialization
            }

            return Ok(legs);
        }

        // GET api/legsapi/5
        [HttpGet("{id}", Name = "GetLeg")]
        public async Task<IActionResult> Get(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Leg leg = await _legRepository.Get(id);

            Driver driver = await _driverRepository.GetAsync(leg.DriverID);
            var authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            leg.Driver = null; // prevent self-referencing loops during serialization

            return Ok(leg);
        }

        // POST api/legsapi/new
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] Leg leg)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Driver driver = await _driverRepository.GetAsync(leg.DriverID);
            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _legRepository.AddAsync(leg);

            leg.Driver = null; // to prevent self-referencing loops in JSON serialization

            return CreatedAtRoute("GetLeg", new { id = leg.LegID }, leg);
        }

        // PUT api/legsapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Leg leg)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Leg existingLeg = await _legRepository.Get(id);
            if (existingLeg == null)
            {
                return NotFound();
            }

            Driver currentDriver = await _driverRepository.GetAsync(existingLeg.DriverID);
            Driver newDriver = await _driverRepository.GetAsync(leg.DriverID);
            AuthorizationResult authResult1 = await _authorizationService.AuthorizeAsync(User, currentDriver, "DriverInfoPolicy");
            AuthorizationResult authResult2 = await _authorizationService.AuthorizeAsync(User, newDriver, "DriverInfoPolicy");

            if (!authResult1.Succeeded || !authResult2.Succeeded)
            {
                return Forbid();
            }

            existingLeg.DriverID = leg.DriverID;
            existingLeg.StartAddress = leg.StartAddress;
            existingLeg.PickupRequestTime = leg.PickupRequestTime;
            existingLeg.StartTime = leg.StartTime;
            existingLeg.DestinationAddress = leg.DestinationAddress;
            existingLeg.ArrivalTime = leg.ArrivalTime;
            existingLeg.Distance = leg.Distance;
            existingLeg.Fare = leg.Fare;
            existingLeg.NumOfPassengersAboard = leg.NumOfPassengersAboard;
            existingLeg.NumOfPassengersPickedUp = leg.NumOfPassengersPickedUp;
            existingLeg.FuelCost = leg.FuelCost;

            await _legRepository.EditAsync(existingLeg);

            return Ok();
        }

        // DELETE api/legsapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Leg leg = await _legRepository.Get(id);
            if (leg == null)
            {
                return NotFound();
            }

            Driver driver = await _driverRepository.GetAsync(leg.DriverID);
            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _legRepository.DeleteAsync(leg);

            return Ok();
        }
    }
}
