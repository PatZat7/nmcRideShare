using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Models.ViewModels
{
	public class RiderSchedulesViewModel
	{
		public IEnumerable<RideRequest> RideRequests { get; set; }
		public IEnumerable<Route> Routes { get; set; }
	}
}