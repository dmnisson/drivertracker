using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DriverTracker.Models;
using DriverTracker.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DriverTracker.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PickupRequestApiController : Controller
    {
        private readonly IPickupRequestRepository _repository;
        private readonly ILegRepository _legRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IAuthorizationService _authorizationService;

        public PickupRequestApiController(IPickupRequestRepository repository,
            ILegRepository legRepository,
            IDriverRepository driverRepository,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _legRepository = legRepository;
            _driverRepository = driverRepository;
            _authorizationService = authorizationService;
        }

        // GET: api/pickuprequestapi
        [HttpGet]
        [Authorize(Roles = "Admin, Driver, PickupRequestSystem")]
        public object Get()
        {
            if (_driverRepository.IsDriver(User))
            {
                // list all pickup requests belonging to the user
                return new
                {
                    PickupRequests = _repository.ListForDriver(_driverRepository.GetDriverModel(User))
                };
            }

            return new
            {
                PickupRequests = _repository.List()
            };
        }

        // GET: api/pickuprequestapi/5
        [HttpGet("{id}", Name = "GetPickupRequest")]
        public async Task<IActionResult> Get(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var driver = _repository.GetAssignedDriver(id);
            if (driver == null)
            {
                driver = new Driver
                {
                    DriverID = -1
                }; // so that authorization handler won't complain if user is admin
            }
            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicyPickupAllowed");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(_repository.Get(id));
        }

        // GET api/pickuprequestapi/assigned/5
        [HttpGet("assigned/{id}")]
        public async Task<IActionResult> GetAssigned(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var driver = _repository.GetAssignedDriver(id);
            if (driver == null)
            {
                return Ok(null);
            }
            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicyPickupAllowed");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(new { driver.DriverID });
        }

        // GET api/pickuprequestapi/answer/5
        [HttpGet("answer/{id}")]
        public async Task<IActionResult> GetAnswer(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            int? driverID = _repository.GetAnswerLeg(id)?.DriverID;
            if (driverID == null) {
                if (User.IsInRole("Driver")) return Forbid();
                return Ok(null);
            }

            var driver = await _driverRepository.GetAsync(driverID.Value);
            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicyPickupAllowed");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(new { _repository.GetAnswerLeg(id).LegID });
        }

        // GET api/pickuprequestapi/forleg/5
        [HttpGet("forleg/{legID}")]
        public async Task<IActionResult> GetForLeg(int legID)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            var leg = await _legRepository.Get(legID);
            if (leg == null)
            {
                return NotFound();
            }

            var driver = await _driverRepository.GetAsync(leg.DriverID);
            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicyPickupAllowed");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(_repository.GetForLeg(leg));
        }

        // POST api/pickuprequestapi
        [HttpPost]
        [Authorize(Roles = "Admin, PickupRequestSystem")]
        public IActionResult PostRequest([FromBody]PickupRequest request)
        {
            _repository.Add(request);

            return CreatedAtRoute("GetPickupRequest", new { id = request.PickupRequestID }, request);
        }

        // PUT api/pickuprequestapi/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, PickupRequestSystem")]
        public IActionResult Put(int id, [FromBody]PickupRequest request)
        {
            request.PickupRequestID = id;
            _repository.Edit(request);

            return Ok();
        }

        // DELETE api/pickuprequestapi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, PickupRequestSystem")]
        public void Delete(int id)
        {
            _repository.Cancel(_repository.Get(id));
        }

        // PUT api/pickuprequest/answer/5
        [HttpPut("answer/{id}")]
        [Authorize(Roles = "Admin, PickupRequestSystem")]
        public void Answer(int id, [FromBody] Leg leg)
        {
            _repository.Answer(_repository.Get(id), _legRepository.Get(leg.LegID).Result);
        }

        // PUT api/pickuprequestapi/assigned/5
        [HttpPut("assigned/{id}")]
        [Authorize(Roles = "Admin, PickupRequestSystem")]
        public void Assign(int id, [FromBody] Driver driver)
        {
            _repository.Assign(_repository.Get(id), _driverRepository.GetAsync(driver.DriverID).Result);
        }
    }
}
