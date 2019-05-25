using System;
using Newtonsoft.Json;

namespace DriverTracker.Models
{
    public class PickupRequest
    {
        public int PickupRequestID { get; set; }
        public DateTime RequestedTime { get; set; }
        public string RequestedAddress { get; set; }

        [JsonIgnore]
        public virtual PickupDriverAssignment Assigned { get; set; }

        [JsonIgnore]
        public virtual AnsweredPickupRequest Answered { get; set; }
    }
}
