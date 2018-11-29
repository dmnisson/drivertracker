using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DriverTracker.Domain;
using DriverTracker.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    public class GeocodingController : Controller
    {
        private readonly IGeocodingDbSync _dbSync;

        public GeocodingController(IGeocodingDbSync dbSync)
        {
            _dbSync = dbSync;
        }

        // GET api/geocoding
        [HttpGet]
        public async Task<LegCoordinates[]> Get()
        {
            return (await _dbSync.ListLegCoordinatesAsync()).ToArray();
        }

        // GET api/geocoding/5
        [HttpGet("{id}")]
        public async Task<LegCoordinates> Get(int id)
        {
            return await _dbSync.GetLegCoordinatesAsync(id);
        }

        // POST api/geocoding/update
        [HttpPost("update")]
        public async void Post()
        {
            await _dbSync.UpdateAllAsync();
        }

        // POST api/geocoding/update/5
        [HttpPost("update/{id}")]
        public async void Post(int id)
        {
            await _dbSync.UpdateLegAsync(id);
        }
    }
}
