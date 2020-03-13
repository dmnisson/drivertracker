using System.ComponentModel.DataAnnotations.Schema;

namespace DriverTracker.Models
{
    public class PickupDriverAssignment
    {
        public int PickupDriverAssignmentID { get; set; }

        [ForeignKey("Driver")]
        public Driver AssignedDriver { get; set; }

        [ForeignKey("PickupRequest")]
        public PickupRequest Request { get; set; }
    }
}
