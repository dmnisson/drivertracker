using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DriverTracker.Models {
	public class Driver {
        public int DriverID { get; set; }

        [Obsolete("Integer User ID is not supported. Please use UserIDString.", true)]
        public int UserID { get; set; }

        public string UserIDString { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "A driver name is required.")]
		public string Name { get; set; }

        [Display(Name = "License Number")]
        [Required(ErrorMessage = "A license number is required.")]
        public string LicenseNumber { get; set; }

        public ICollection<Analysis> Analyses { get; set; }
        public ICollection<Leg> Legs { get; set; }
        public ICollection<PickupDriverAssignment> PickupDriverAssignments { get; set; }
    }
}
