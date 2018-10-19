using System;
using System.Collections.Generic;

namespace DriverTracker.Models {
	public class Driver {
        public int DriverID { get; set; }
        public int UserID { get; set; }
		public string Name { get; set; }
        public string LicenseNumber { get; set; }

        public ICollection<Analysis> Analyses { get; set; }
        public ICollection<Leg> Legs { get; set; }
	}
}
