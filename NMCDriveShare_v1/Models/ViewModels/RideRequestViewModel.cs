using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Models.ViewModels
{
	public class RideRequestViewModel
	{
		//public string RiderId { get; set; }

		public int RequestNum { get; set; }

		public string CheckedSunday { get; set; }

		public string CheckedMonday { get; set; }

		public string CheckedTuesday { get; set; }

		public string CheckedWednesday { get; set; }

		public string CheckedThursday { get; set; }

		public string CheckedFriday { get; set; }

		public string CheckedSaturday { get; set; }

		[Required]
		public TimeSpan ArrivalTime { get; set; }

		[Required]
		public TimeSpan DepartingTime { get; set; }

		[Required]
		public string DestinationName { get; set; }

		[Required]
		public string SourceName { get; set; }

		public int? DestinationId { get; set; }

		public int? SourceId { get; set; }

		public bool IsScheduled { get; set; }
	}
}