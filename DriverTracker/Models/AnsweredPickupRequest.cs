using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverTracker.Models
{
    public class AnsweredPickupRequest
    {
        public int AnsweredPickupRequestID { get; set; }

        [ForeignKey("Leg")]
        public Leg AnswerLeg { get; set; }

        [ForeignKey("PickupRequest")]
        public PickupRequest Request { get; set; }
    }
}
