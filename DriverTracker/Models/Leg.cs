using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DriverTracker.Models
{
    public class Leg
    {
        public int LegID { get; set; }
        public int DriverID { get; set; }

        [Display(Name = "Pickup Address")]
        [Required(ErrorMessage = "A leg must have a pickup address.")]
        public string StartAddress { get; set; }

        [Display(Name = "Requested Pickup Time")]
        public DateTime? PickupRequestTime { get; set; }

        [Display(Name = "Pickup Time")]
        [Required(ErrorMessage = "A leg must have a pickup time.")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Destination Address")]
        [Required(ErrorMessage = "A leg must end somewhere.")]
        public string DestinationAddress { get; set; }

        [Display(Name = "Arrival Time")]
        [Required(ErrorMessage = "A leg must end some time.")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Distance")]
        [Required(ErrorMessage = "A leg must have a distance.")]
        [Range(0, int.MaxValue, ErrorMessage = "Distance cannot be negative.")]
        public decimal Distance { get; set; }

        [Display(Name = "Fare")]
        [Required(ErrorMessage = "A fare value is required. Enter 0 for free rides.")]
        [Range(0, int.MaxValue, ErrorMessage = "Fare cannot be negative.")]
        public decimal Fare { get; set; } // per-passenger fare

        [Display(Name = "Passengers Aboard")]
        [Required(ErrorMessage = "A number of passengers is required. Enter 0 for non-passenger legs.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of passengers cannot be negative.")]
        public int NumOfPassengersAboard { get; set; }

        [Display(Name = "Pickups")]
        [Required(ErrorMessage = "A number of passengers is required. Enter 0 for non-passenger legs.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of pickups cannot be negative.")]
        [LessThanOrEqualTo("NumOfPassengersAboard", ErrorMessage = "More passengers cannot have been picked up than are now aboard.")]
        public int NumOfPassengersPickedUp { get; set; }

        [Display(Name = "Fuel Cost")]
        [Required(ErrorMessage = "A fuel cost is required.")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Fuel cost cannot be negative.")]
        public decimal FuelCost { get; set; } // fuel cost per mile

        public Driver Driver { get; set; }

        public virtual LegCoordinates LegCoordinates { get; set; }

        public Leg GetPreviousLeg() {
            return this.Driver.Legs.Where(leg => leg.ArrivalTime <= this.StartTime)
                       .OrderByDescending(leg => leg.ArrivalTime).First();
        }

        public decimal GetTotalFuelCost() {
            return this.FuelCost * this.Distance;
        }
    }
}
