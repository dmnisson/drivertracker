using System;

namespace DriverTracker.Models {
	public class Driver {
        public int ID { get; set; }
        public int UserID { get; set; }
		public string Name { get; set; }
        public string LicenseNumber { get; set; }
	}
}
