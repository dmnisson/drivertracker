using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accord.Statistics.Models.Regression;

using DriverTracker.Models;
using DriverTracker.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DriverTracker.Controllers
{
    [Authorize(Roles = "Admin,Analyst")]
    [Route("api/[controller]")]
    public class AnalysisApiController : Controller
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILocationClustering _locationClustering;
        private readonly ILegRepository _legRepository;
        private readonly IPickupPrediction _pickupPrediction;
        private readonly DriverStatistics _driverStatistics;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MvcDriverContext _context;

        public AnalysisApiController(IDriverRepository driverRepository,
                                     ILegRepository legRepository,
                                     ILocationClustering locationClustering,
                                     IPickupPrediction pickupPrediction,
                                     UserManager<IdentityUser> userManager,
                                     MvcDriverContext context
            ) {
            _driverRepository = driverRepository;
            _legRepository = legRepository;
            _locationClustering = locationClustering;
            _pickupPrediction = pickupPrediction;
            _driverStatistics = new DriverStatistics(driverRepository, legRepository);
            _userManager = userManager;
            _context = context;
        }

        // GET: api/analysisapi
        [HttpGet]
        public async Task<IEnumerable<DriverStatisticResults>> Get()
        {
            List<DriverStatisticResults> driverStatisticResults = new List<DriverStatisticResults>();

            IEnumerable<Driver> drivers = await _driverRepository.ListAsync();


            foreach (Driver driver in drivers) {
                _driverStatistics.ComputeDriverStatistics(driver.DriverID);
                driverStatisticResults.Add(_driverStatistics.GetDriverStatisticResults(driver.DriverID));
            }

            return driverStatisticResults;
        }

        // GET api/analysisapi/company
        [HttpGet("company")]
        public DriverStatistics GetCompany()
        {
            _driverStatistics.ComputeCompanyStatistics();
            return _driverStatistics;
        }

        // GET api/analysisapi/5
        [HttpGet("{id}")]
        public DriverStatisticResults Get(int id)
        {
            _driverStatistics.ComputeDriverStatistics(id);
            return _driverStatistics.GetDriverStatisticResults(id);
        }

        // GET api/analysisapi/logistic/5
        [HttpGet("logistic/{id}")]
        public async Task<LogisticRidershipPredictionResult> GetLogistic(int id)
        {
            RidershipPrediction farePrediction = new RidershipPrediction(_legRepository, id);
            DateTime fromDateTime = DateTime.Now.AddMonths(-12);
            DateTime toDateTime = DateTime.Now;
            await farePrediction.LearnFromDates(fromDateTime, toDateTime);

            LogisticRidershipPredictionResult result = new LogisticRidershipPredictionResult
            {
                DriverID = id,
                FromDateTime = fromDateTime,
                ToDateTime = toDateTime,
                RegressionResult = farePrediction.GetRegressionModels()
            };

            return result;
        }

        // GET api/analysisapi/multipickupprob/5/6/12/13.7/
        [HttpGet("multipickupprob/{id}/{delay}/{duration}/{fare}/")]
        public async Task<double[]> GetMultiPickupProb(int id, double delay, double duration, double fare) {
            RidershipPrediction farePrediction = new RidershipPrediction(_legRepository, id);
            DateTime fromDateTime = DateTime.Now.AddMonths(-12);
            DateTime toDateTime = DateTime.Now;
            await farePrediction.LearnFromDates(fromDateTime, toDateTime);

            return farePrediction.RidershipClassProbabilities(delay, duration, fare);
        }

        // GET api/analysisapi/fareclassprob/39.7/-121.8/38.5/-121.9/1.3/26/2/160
        [HttpGet("fareclassprob/{startlat}/{startlon}/{endlat}/{endlon}/{delay}/{duration}/{pickups}/{interval}")]
        public async Task<double[]> GetFareClassProb(double startlat, double startlon, double endlat, double endlon, double delay, double duration, int pickups, double interval)
        {
            await TrainPickupPrediction();

            return _pickupPrediction.GetFareClassProbabilities(
                new double[] { startlat, startlon },
                new double[] { endlat, endlon },
                delay, duration, pickups, interval
                );
        }

        private async Task TrainPickupPrediction()
        {

            await SetFareClassIntervals();

            // train
            DateTime fromDateTime = DateTime.Now.AddMonths(-12);
            DateTime toDateTime = DateTime.Now;
            await _pickupPrediction.LearnFromDates(fromDateTime, toDateTime);
        }

        private async Task SetFareClassIntervals()
        {
            // set the fare class intervals from the database
            string UserId = _userManager.GetUserId(User);
            _pickupPrediction.FareClassIntervals = (await _context.Analysts
                .FirstOrDefaultAsync(a => a.UserIDString == UserId)
                ).FareClassIntervals;
        }

        // GET api/analysisapi/pickupprob/39.7/-121.8/38.5/-121.9/1.3/26/13.10/160
        [HttpGet("pickupprob/{startlat}/{startlon}/{endlat}/{endlon}/{delay}/{duration}/{fare}/{interval}")]
        public async Task<double[]> GetPickupProb(double startlat, double startlon, double endlat, double endlon, double delay, double duration, double fare, double interval)
        {
            await TrainPickupPrediction();

            return _pickupPrediction.GetPickupProbabilities(
                
                
                new double[] { startlat, startlon },
                new double[] { endlat, endlon },
                delay, duration, fare, interval
                );
        }

        // GET api/analysisapi/fareclassintervals
        [HttpGet("fareclassintervals")]
        public async Task<double[]> GetFareClassIntervals()
        {
            await SetFareClassIntervals();
            return _pickupPrediction.FareClassIntervals.ToArray();
        }

        // PUT api/analysisapi/fareclassintervals
        [HttpPut("fareclassintervals")]
        public async Task<IActionResult> PutFareClassIntervals([FromBody] double[] intervalBounds)
        {

            // current analyst
            Analyst analyst = await _context.Analysts.FirstOrDefaultAsync(
                a => a.UserIDString == _userManager.GetUserId(User));
            if (analyst == null) return Forbid();

            analyst.FareClassIntervals = intervalBounds;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET api/analysisapi/renumberclustring
        [HttpGet("renumberclustering")]
        public async Task<IActionResult> PutRenumberClustering()
        {
            await _locationClustering.RenumberAsync();

            return Ok(_locationClustering.ClusterCollection.Count);
        }
    }
}
