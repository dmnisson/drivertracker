using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using DriverTracker.Domain;
using DriverTracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DriverTracker.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GeocodingController : Controller
    {
        private readonly IGeocodingDbSync _dbSync;
        private readonly ILegRepository _legRepository; // for authorization purposes
        private readonly IAuthorizationService _authorizationService;

        public GeocodingController(
            IGeocodingDbSync dbSync,
            ILegRepository legRepository,
            IAuthorizationService authorizationService)
        {
            _dbSync = dbSync;
            _legRepository = legRepository;
            _authorizationService = authorizationService;
        }

        // GET api/geocoding
        [HttpGet]
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IActionResult> Get()
        {
            return Ok((await _dbSync.ListLegCoordinatesAsync()).ToArray());
        }

        // GET api/geocoding/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var driver = (await _legRepository.Get(id)).Driver;
            var authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(await _dbSync.GetLegCoordinatesAsync(id));
        }

        // GET api/geocoding/direct/670%20Sycamore
        [HttpGet("direct/{address}")]
        [Authorize]
        public async Task<IActionResult> Get(string address)
        {
            IEnumerable<Geocoding.Address> geoAddress = await _dbSync.Geocoder.GeocodeAsync(address);
            return Ok(new double[] { 
                geoAddress.Average(a => a.Coordinates.Latitude),
                geoAddress.Average(a => a.Coordinates.Longitude) 
                });
        }

        // POST api/geocoding/update
        [HttpPost("update")]
        [Authorize]
        public async Task Post()
        {
            await _dbSync.UpdateAllAsync();
        }

        // POST api/geocoding/update/5
        [HttpPost("update/{id}")]
        [Authorize]
        public async Task Post(int id)
        {
            await _dbSync.UpdateLegAsync(id);
        }
    }
}
