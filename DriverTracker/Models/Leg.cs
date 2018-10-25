using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverTracker.Models
{
    public class Leg
    {
        public int LegID { get; set; }
        public int DriverID { get; set; }

        public string StartAddress { get; set; }
        public DateTime? PickupRequestTime { get; set; }
        public DateTime StartTime { get; set; }
        public string DestinationAddress { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Distance { get; set; }
        public decimal Fare { get; set; } // per-passenger fare
        public int NumOfPassengersAboard { get; set; }
        public int NumOfPassengersPickedUp { get; set; }

        public Driver Driver { get; set; }

        public Leg GetPreviousLeg() {
            return this.Driver.Legs.Where(leg => leg.ArrivalTime <= this.StartTime)
                       .OrderByDescending(leg => leg.ArrivalTime).First();
        }
    }
}
