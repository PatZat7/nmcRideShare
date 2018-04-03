using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Models.ViewModels
{
	public class AddRideViewModel
	{
		public string CheckedSunday { get; set; }
		public string CheckedMonday { get; set; }
		public string CheckedTuesday { get; set; }
		public string CheckedWednesday { get; set; }
		public string CheckedThursday { get; set; }
		public string CheckedFriday { get; set; }
		public string CheckedSaturday { get; set; }
		public string ArrivalTime { get; set; }
		public string DepartingTime { get; set; }
	}
}