using System;
using System.ComponentModel.DataAnnotations;

namespace NMCDriveShare_v1.Models.ViewModels
{
	public class RouteViewModel
	{
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

		public bool IsScheduled { get; set; }

		[Range(0, 100)]
		[Display(Name = "Max Seat Count")]
		public int? MaxSeatCount { get; set; }

		public int? DestinationId { get; set; }
	}
}