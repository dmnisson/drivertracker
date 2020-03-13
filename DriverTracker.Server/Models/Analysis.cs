using System;
namespace DriverTracker.Models
{
    public class Analysis
    {
        public int AnalysisID { get; set; }
        public int AnalystID { get; set; }
        public int DriverID { get; set; }

        public Analyst Analyst { get; set; }
        public Driver Driver { get; set; }
    }
}
