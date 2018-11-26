using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverTracker.Models
{
    public class LegCoordinates
    {
        [Key, ForeignKey("Leg")]
        public int LegID { get; set; }

        public decimal StartLatitude { get; set; }
        public decimal StartLongitude { get; set; }
        public decimal DestLatitude { get; set; }
        public decimal DestLongitude { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
