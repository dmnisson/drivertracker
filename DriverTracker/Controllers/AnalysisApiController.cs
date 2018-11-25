using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accord.Statistics.Models.Regression;

using DriverTracker.Models;
using DriverTracker.Domain;

namespace DriverTracker.Controllers
{
    [Authorize(Roles = "Admin,Analyst")]
    [Route("api/[controller]")]
    public class AnalysisApiController : Controller
    {
        private readonly MvcDriverContext _context;
        private readonly DriverStatistics _driverStatistics;

        public AnalysisApiController(MvcDriverContext context) {
            _context = context;
            _driverStatistics = new DriverStatistics(context);
        }

        // GET: api/analysisapi
        [HttpGet]
        public IEnumerable<DriverStatisticResults> Get()
        {
            List<DriverStatisticResults> driverStatisticResults = new List<DriverStatisticResults>();

            IEnumerable<Driver> drivers = _context.Drivers.AsEnumerable();


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
        public LogisticRidershipPredictionResult GetLogistic(int id)
        {
            RidershipPrediction farePrediction = new RidershipPrediction(_context, id);
            DateTime fromDateTime = DateTime.Now.AddMonths(-12);
            DateTime toDateTime = DateTime.Now;
            farePrediction.LearnFromDates(fromDateTime, toDateTime);

            LogisticRidershipPredictionResult result = new LogisticRidershipPredictionResult();
            result.DriverID = id;
            result.FromDateTime = fromDateTime;
            result.ToDateTime = toDateTime;
            result.RegressionResult = farePrediction.GetRegressionModels();

            return result;
        }

        // GET api/analysisapi/multipickupprob/5/6/12/13.7/
        [HttpGet("multipickupprob/{id}/{delay}/{duration}/{fare}/")]
        public double[] GetMultiPickupProb(int id, double delay, double duration, double fare) {
            RidershipPrediction farePrediction = new RidershipPrediction(_context, id);
            DateTime fromDateTime = DateTime.Now.AddMonths(-12);
            DateTime toDateTime = DateTime.Now;
            farePrediction.LearnFromDates(fromDateTime, toDateTime);

            return farePrediction.RidershipClassProbabilities(delay, duration, fare);
        }
    }
}
