using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Models.ViewModels
{
	public class UserSchedulesViewModel
	{
		public string Username { get; set; }
		public string Gender { get; set; }
		public bool IsDriver { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IEnumerable<RideRequestViewModel> RideRequests { get; set; }
		public IEnumerable<RouteViewModel> Routes { get; set; }
	}
}