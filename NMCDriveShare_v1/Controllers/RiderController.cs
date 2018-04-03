using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCDriveShare_v1.Models;
using Microsoft.AspNet.Identity;
using NMCDriveShare_v1.Models.ViewModels;

namespace NMCDriveShare_v1.Controllers
{
	[Authorize]
    public class RiderController : Controller
    {
		private readonly DriveShareEntities3 _dbContext = new DriveShareEntities3();

		// GET: Rider
		public ActionResult Index()
        {
			return View(generateIndexViewModel());
        }

		[HttpPost]
		public ActionResult AddRide(AddRideViewModel viewModel)
		{
			// select user
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			// select id for a new schedule
			int scheduleId =
				// select last schedule in table
				(_dbContext.Schedules.OrderByDescending(s => s.ScheduleId).FirstOrDefault()
				// otherwise, start with 0
				?? new Schedule() { ScheduleId = 0 })
				// increment id by one
				.ScheduleId + 1;

			// create a new schedule
			Schedule rideSchedule = new Schedule()
			{
				ScheduleId = scheduleId,
				CheckedSunday = viewModel.CheckedSunday == "on" ? true : false,
				CheckedMonday = viewModel.CheckedMonday == "on" ? true : false,
				CheckedTuesday = viewModel.CheckedTuesday == "on" ? true : false,
				CheckedWednesday = viewModel.CheckedWednesday == "on" ? true : false,
				CheckedThursday = viewModel.CheckedThursday == "on" ? true : false,
				CheckedFriday = viewModel.CheckedFriday == "on" ? true : false,
				CheckedSaturday = viewModel.CheckedSaturday == "on" ? true : false
			};

			// create a ride request
			RideRequest rideRequest = null;

			try
			{
				rideRequest = new RideRequest()
				{
					// parse arrival time and departing time as a time
					ArrivalTime = TimeSpan.Parse(viewModel.ArrivalTime),
					DepartingTime = TimeSpan.Parse(viewModel.DepartingTime),
					RiderId = user.Id,
					ScheduleId = rideSchedule.ScheduleId,
					RequestNum = (_dbContext.RideRequests
							.OrderByDescending(rr => rr.RiderId == userId)
							.FirstOrDefault()
						?? new RideRequest() { RequestNum = 0 })
						.RequestNum + 1,
					RideRequestStatu = _dbContext.RideRequestStatus
						.FirstOrDefault(s => s.RequestStatusName == "None")
				};
			}
			catch
			{
				ViewBag.AddRideErrorMessage = "Could not parse provided time correctly.";
				return RedirectToAction(nameof(Index), "Rider");
			}

			// check request times
			if (rideRequest.DepartingTime.CompareTo(rideRequest.ArrivalTime) > 0)
			{
				// return error if departing time is later than arrival time
				ViewBag.AddRideErrorMessage = "Departing time cannot be later than arrival time.";
				return RedirectToAction(nameof(Index), "Rider");
			}

			if (rideRequest != null)
			{
				// add schedule to database first
				_dbContext.Schedules.Add(rideSchedule);
				_dbContext.SaveChanges();

				_dbContext.RideRequests.Add(rideRequest);
				_dbContext.SaveChanges();
			}
			else
			{
				ViewBag.AddRideErrorMessage = "Could not add the desired ride request. Please try again.";
				return RedirectToAction(nameof(Index), "Rider");
			}

			return RedirectToAction(nameof(Index), "Rider");
		}

		[HttpPost]
		public ActionResult RemoveRide(int requestNum)
		{
			// select user
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			// find the request in the db
			RideRequest selection = _dbContext.RideRequests.Where(rr => rr.RiderId == userId).FirstOrDefault(rr => rr.RequestNum == requestNum);

			if (selection != null)
			{
				// remove selection's schedule
				Schedule schedule = selection.Schedule;

				// remove the selection if exists
				_dbContext.RideRequests.Remove(selection);
				_dbContext.SaveChanges();

				_dbContext.Schedules.Remove(schedule);
				_dbContext.SaveChanges();
			}

			// return to the rider home page
			return RedirectToAction(nameof(Index));
		}

		private RiderSchedulesViewModel generateIndexViewModel()
		{
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			return new RiderSchedulesViewModel()
			{
				RideRequests = user.RideRequests,
				Routes = user.RoutesDriving
			};
		}
	}
}