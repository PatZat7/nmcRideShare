using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMCDriveShare_v1.Models;
using Microsoft.AspNet.Identity;
using NMCDriveShare_v1.Models.ViewModels;
using NMCDriveShare_v1.Utilities;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace NMCDriveShare_v1.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly DriveShareEntities3 _dbContext = new DriveShareEntities3();

		public UserController()
		{
			//if (User != null) ViewBag.IsDriver = UserStatusChecker.CheckDriverStatus(_dbContext, User.Identity.GetUserId());
		}

		// GET: Rider
		public ActionResult Index()
		{
			ViewBag.GeolocationPairs = buildGeolocationDictionary();
			return View(generateIndexViewModel());
		}

		#region JSON ACTIONS
		[HttpPost]
		public ActionResult ChangeUserDriverStatus(string isDriving)
		{
			string message = "";
			bool status;

			if (isDriving == "true")
			{
				status = true;
			}
			else
			{
				status = false;
			}

			// get current user
			string userId = User.Identity.GetUserId();
			AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			// send error if current user is not in db
			if (currentUser == null)
			{
				message = "Failed: User not found.";
			}

			try
			{
				// attempt to change his/her status
				currentUser.IsDriver = status;
				_dbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				// send error if concurrency exception occurs
				message = "Failed: Error occurred while making changes.";
			}

			// if successful, send back confirmation message
			message = "User status changed.";

			// send back a Json object with a message
			return Json(new { result = message });
		}

		[HttpPost]
		public ActionResult ChangeRideScheduledStatus(int rideNum, bool isScheduled)
		{
			string message;

			// get current user
			string userId = User.Identity.GetUserId();
			AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			if (currentUser == null)
			{
				message = "Failed: User not found.";
			}

			// get targeted ride request
			RideRequest selectedRideRequest = _dbContext.RideRequests.FirstOrDefault(rr => rr.RiderId == userId && rr.RequestNum == rideNum);

			if (selectedRideRequest == null)
			{
				message = "Failed: Ride request not found.";
			}

			// attempt to update the ride request
			try
			{
				// load in the ride request status objects
				RideRequestStatu rideStatusNone = _dbContext.RideRequestStatus
					.AsNoTracking()
					.FirstOrDefault(rs => rs.RequestStatusName == "None");
				RideRequestStatu rideStatusPending = _dbContext.RideRequestStatus
					.AsNoTracking()
					.FirstOrDefault(rs => rs.RequestStatusName == "Pending");

				if (isScheduled)
				{
					// set ride request status to "Pending", or "scheduled"
					selectedRideRequest.RideRequestStatu = rideStatusPending;
					selectedRideRequest.RequestStatusId = rideStatusPending.RequestStatusId;
				}
				else
				{
					// set ride request status to "None", or "canceled"
					selectedRideRequest.RideRequestStatu = rideStatusNone;
					selectedRideRequest.RequestStatusId = rideStatusNone.RequestStatusId;
				}

				// try to save changes
				_dbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				message = "Failed: Error occurred while making changes.";
			}

			// if successful, send back confirmation message
			message = "Ride request status changed.";

			// return JSON object with message
			return Json(new { result = message });
		}

		[HttpPost]
		public ActionResult ChangeUserVisibility(bool active)
		{
			string message = "";

			// get current user
			string userId = User.Identity.GetUserId();
			AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			// send error if current user is not in db
			if (currentUser == null)
			{
				message = "Failed: User not found.";
			}

			try
			{
				// attempt to change his/her status
				currentUser.IsActive = active;
				_dbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				// send error if concurrency exception occurs
				message = "Failed: Error occurred while making changes.";
			}

			// if successful, send back confirmation message
			message = "User visibility status changed.";

			// send back a Json object with a message
			return Json(new { result = message });
		}
		#endregion

		#region RIDE REQUESTS
		[HttpPost]
		public ActionResult AddRide(RideRequestViewModel viewModel)
		{
			// check request times
			if (viewModel.DepartingTime.CompareTo(viewModel.ArrivalTime) > 0)
			{
				// return error if departing time is later than arrival time
				ViewBag.AddRideErrorMessage = "Departing time cannot be later than arrival time.";
				ViewBag.GeolocationPairs = buildGeolocationDictionary();
				return View(nameof(Index), generateIndexViewModel());
			}

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

			// look for a dropoff location marker
			Geolocation dropOffLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == viewModel.DestinationName);
			// if none exists, make one with the new dropoff location
			if (dropOffLocation == null)
			{
				dropOffLocation = createDestinationGeolocation(viewModel.DestinationName);

				// save changes
				_dbContext.Geolocations.Add(dropOffLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}
			

			// look for a pickup location marker
			Geolocation pickupLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == viewModel.SourceName);
			// if none exists, make one with the new pickup location
			if (pickupLocation == null)
			{
				pickupLocation = createDestinationGeolocation(viewModel.SourceName);

				// save changes
				_dbContext.Geolocations.Add(pickupLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}

			// create a ride request
			RideRequest rideRequest = null;

			try
			{
				rideRequest = new RideRequest()
				{
					// parse arrival time and departing time as a time
					ArrivalTime = viewModel.ArrivalTime,
					DepartingTime = viewModel.DepartingTime,
					RiderId = user.Id,
					Rider = user,
					ScheduleId = rideSchedule.ScheduleId,
					Schedule = rideSchedule,
					RequestNum = (_dbContext.RideRequests
							.Where(rr => rr.RiderId == userId)
							.OrderByDescending(rr => rr.RequestNum)
							.FirstOrDefault()
						?? new RideRequest() { RequestNum = 0 })
						.RequestNum + 1,
					RequestStatusId = _dbContext.RideRequestStatus
						.FirstOrDefault(s => s.RequestStatusName == "None")
						.RequestStatusId,
					RideRequestStatu = _dbContext.RideRequestStatus
						.FirstOrDefault(s => s.RequestStatusName == "None"),
					DropoffLocationId = dropOffLocation.LocationId,
					DropoffLocation = dropOffLocation,
					PickupLocationId = pickupLocation.LocationId,
					PickupLocation = pickupLocation
				};
			}
			catch
			{
				ViewBag.AddRideErrorMessage = "Could not parse provided time correctly.";
				ViewBag.GeolocationPairs = buildGeolocationDictionary();
				return View(nameof(Index), generateIndexViewModel());
			}

			if (rideRequest != null)
			{
				// add schedule to database first
				_dbContext.Schedules.Add(rideSchedule);
				_dbContext.SaveChanges();

				_dbContext.RideRequests.Add(rideRequest);
				_dbContext.SaveChanges();
			}
			// if catch fails or falls through
			else
			{
				ViewBag.AddRideErrorMessage = "Could not add the desired ride request. Please try again.";
				ViewBag.GeolocationPairs = buildGeolocationDictionary();
				return View(nameof(Index), generateIndexViewModel());
			}

			return RedirectToAction(nameof(Index), "User");
		}

		[HttpPost]
		public ActionResult EditRide(int id, RideRequestViewModel rideRequestView)
		{
			if (id != rideRequestView.RequestNum)
			{
				return HttpNotFound();
			}

			// check request times
			if (rideRequestView.DepartingTime.CompareTo(rideRequestView.ArrivalTime) > 0)
			{
				// return error if departing time is later than arrival time
				ViewBag.AddRideErrorMessage = "Departing time cannot be later than arrival time.";
				return RedirectToAction(nameof(Index), "User");
			}

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

			// look for a dropoff location marker
			Geolocation dropOffLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == rideRequestView.DestinationName);
			// if none exists, make one with the new dropoff location
			if (dropOffLocation == null)
			{
				dropOffLocation = createDestinationGeolocation(rideRequestView.DestinationName);

				// save changes
				_dbContext.Geolocations.Add(dropOffLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}

			// look for a pickup location marker
			Geolocation pickupLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == rideRequestView.SourceName);
			// if none exists, make one with the new pickup location
			if (pickupLocation == null)
			{
				pickupLocation = createDestinationGeolocation(rideRequestView.SourceName);

				// save changes
				_dbContext.Geolocations.Add(pickupLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}

			// get the existing ride request
			RideRequest rideRequest = _dbContext.RideRequests
				.Include("Schedule")
				.FirstOrDefault(r => r.RequestNum == rideRequestView.RequestNum);
			// get the schedule for the ride request
			Schedule rideSchedule = rideRequest.Schedule;

			// if none exists, make a new one
			if (rideRequest == null)
			{
				rideRequest = new RideRequest()
				{
					RiderId = userId,
					Rider = user,
					RequestNum = rideRequestView.RequestNum,
					ArrivalTime = rideRequestView.ArrivalTime,
					DepartingTime = rideRequestView.DepartingTime,
					RideRequestStatu = _dbContext.RideRequestStatus.FirstOrDefault(rs => rs.RequestStatusName == "None"),
					RequestStatusId = 0
				};

				int nextScheduleId = 1;
				Schedule lastSchedule = _dbContext.Schedules.OrderByDescending(s => s.ScheduleId).FirstOrDefault();
				if (lastSchedule != null) nextScheduleId = lastSchedule.ScheduleId + 1;

				rideSchedule = new Schedule
				{
					ScheduleId = nextScheduleId,
					CheckedSunday = rideRequestView.CheckedSunday == "on" ? true : false,
					CheckedMonday = rideRequestView.CheckedMonday == "on" ? true : false,
					CheckedTuesday = rideRequestView.CheckedTuesday == "on" ? true : false,
					CheckedWednesday = rideRequestView.CheckedWednesday == "on" ? true : false,
					CheckedThursday = rideRequestView.CheckedThursday == "on" ? true : false,
					CheckedFriday = rideRequestView.CheckedFriday == "on" ? true : false,
					CheckedSaturday = rideRequestView.CheckedSaturday == "on" ? true : false,
				};

				try
				{
					_dbContext.Schedules.Add(rideSchedule);
					_dbContext.SaveChanges();

					_dbContext.RideRequests.Add(rideRequest);
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}
			// update the existing ride request
			else
			{
				// if null schedule, make a new one
				if (rideSchedule == null)
				{
					rideSchedule = new Schedule
					{
						ScheduleId = rideSchedule.ScheduleId
					};

					try
					{
						_dbContext.Schedules.Add(rideSchedule);
						_dbContext.SaveChanges();
					}
					catch
					{
						return View("Error");
					}
				}

				// update the schedule
				rideSchedule.CheckedSunday = rideRequestView.CheckedSunday == "on" ? true : false;
				rideSchedule.CheckedMonday = rideRequestView.CheckedMonday == "on" ? true : false;
				rideSchedule.CheckedTuesday = rideRequestView.CheckedTuesday == "on" ? true : false;
				rideSchedule.CheckedWednesday = rideRequestView.CheckedWednesday == "on" ? true : false;
				rideSchedule.CheckedThursday = rideRequestView.CheckedThursday == "on" ? true : false;
				rideSchedule.CheckedFriday = rideRequestView.CheckedFriday == "on" ? true : false;
				rideSchedule.CheckedSaturday = rideRequestView.CheckedSaturday == "on" ? true : false;
				_dbContext.SaveChanges();
			}

			try
			{
				// update the ride request here
				rideRequest.ArrivalTime = rideRequestView.ArrivalTime;
				rideRequest.DepartingTime = rideRequestView.DepartingTime;
				rideRequest.DropoffLocationId = dropOffLocation.LocationId;
				rideRequest.DropoffLocation = dropOffLocation;
				rideRequest.PickupLocationId = pickupLocation.LocationId;
				rideRequest.PickupLocation = pickupLocation;
				rideRequest.ScheduleId = rideSchedule.ScheduleId;
				rideRequest.Schedule = rideSchedule;
				_dbContext.SaveChanges();
			}
			catch
			{
				ViewBag.AddRideErrorMessage = "Could not parse provided time correctly.";
				return RedirectToAction(nameof(Index), "User");
			}

			if (rideRequest == null)
			{
				ViewBag.AddRideErrorMessage = "Could not add the desired ride request. Please try again.";
				return RedirectToAction(nameof(Index), "User");
			}

			return RedirectToAction(nameof(Index), "User");
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

		[NonAction]
		private ICollection<RideRequestViewModel> buildRideRequestViewModelList(IEnumerable<RideRequest> rideRequests)
		{
			// get the current user
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers
				.FirstOrDefault(u => u.Id == userId);

			if (user == null)
			{
				throw new Exception("Current user not found in database.");
			}

			// generate a lookup to use to select geolocation names
			var geolocationLookup = _dbContext.Geolocations.ToDictionary(g => g.LocationId, g => g.Description);
			IList<RideRequestViewModel> viewModels = new List<RideRequestViewModel>();

			// for each ride request supplied,
			foreach (RideRequest rr in rideRequests)
			{
				// create a view model
				RideRequestViewModel model = new RideRequestViewModel
				{
					//RiderId = currentUser.Id,
					RequestNum = rr.RequestNum,
					ArrivalTime = rr.ArrivalTime,
					DepartingTime = rr.DepartingTime,
					CheckedSunday = rr.Schedule.CheckedSunday ? "on" : null,
					CheckedMonday = rr.Schedule.CheckedMonday ? "on" : null,
					CheckedTuesday = rr.Schedule.CheckedTuesday ? "on" : null,
					CheckedWednesday = rr.Schedule.CheckedWednesday ? "on" : null,
					CheckedThursday = rr.Schedule.CheckedTuesday ? "on" : null,
					CheckedFriday = rr.Schedule.CheckedFriday ? "on" : null,
					CheckedSaturday = rr.Schedule.CheckedSaturday ? "on" : null,
					DestinationId = rr.DropoffLocationId,
					SourceId = rr.PickupLocationId,
					IsScheduled = rr.RequestStatusId > 0
				};

				// get drop off name from the lookup
				if (rr.DropoffLocationId != null)
					model.DestinationName = geolocationLookup[rr.DropoffLocationId.Value];
				else
					model.DestinationName = "";

				// get pick up name from the lookup
				if (rr.PickupLocationId != null)
					model.SourceName = geolocationLookup[rr.PickupLocationId.Value];
				else
					model.SourceName = "";

				// add it to the list
				viewModels.Add(model);
			}

			return viewModels;
		}

		[NonAction]
		private RideRequestViewModel buildRideRequestViewModel(RideRequest rideRequest)
		{
			return buildRideRequestViewModelList(new RideRequest[1] { rideRequest }).ElementAt(0);
		}
		#endregion

		#region ROUTES
		[HttpPost]
		public ActionResult AddRoute(RouteViewModel viewModel)
		{
			// check request times
			if (viewModel.DepartingTime.CompareTo(viewModel.ArrivalTime) > 0)
			{
				// return error if departing time is later than arrival time
				ViewBag.AddRideErrorMessage = "Departing time cannot be later than arrival time.";
				ViewBag.GeolocationPairs = buildGeolocationDictionary();
				return View(nameof(Index), generateIndexViewModel());
			}

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

			// look for a dropoff location marker
			Geolocation dropOffLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == viewModel.DestinationName);
			// if none exists, make one with the new dropoff location
			if (dropOffLocation == null)
			{
				dropOffLocation = createDestinationGeolocation(viewModel.DestinationName);

				// save changes
				_dbContext.Geolocations.Add(dropOffLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}

			// create a ride request
			Route rideRequest = null;

			//try
			//{
			rideRequest = new Route()
			{
				// parse arrival time and departing time as a time
				ArriveTime = viewModel.ArrivalTime,
				DepartTime = viewModel.DepartingTime,
				DriverId = user.Id,
				Driver = user,
				ScheduleId = rideSchedule.ScheduleId,
				Schedule = rideSchedule,
				RouteNum = (_dbContext.Routes
						.Where(rr => rr.DriverId == userId)
						.OrderByDescending(rr => rr.RouteNum)
						.FirstOrDefault()
					?? new Route() { RouteNum = 0 })
					.RouteNum + 1,
				//RequestStatusId = _dbContext.RideRequestStatus
				//	.FirstOrDefault(s => s.RequestStatusName == "None")
				//	.RequestStatusId,
				//RideRequestStatu = _dbContext.RideRequestStatus
				//	.FirstOrDefault(s => s.RequestStatusName == "None"),
				DestinationId = dropOffLocation.LocationId,
				Geolocation = dropOffLocation,
				MaxSeatCount = viewModel.MaxSeatCount
			};
			//}
			//catch
			//{
			//	ViewBag.AddRideErrorMessage = "Could not parse provided time correctly.";
			//	ViewBag.GeolocationPairs = buildGeolocationDictionary();
			//	return View(nameof(Index), generateIndexViewModel());
			//}

			// add schedule to database first
			_dbContext.Schedules.Add(rideSchedule);
			_dbContext.SaveChanges();

			_dbContext.Routes.Add(rideRequest);
			_dbContext.SaveChanges();


			return RedirectToAction(nameof(Index), "User");
		}

		[HttpPost]
		public ActionResult EditRoute(int id, RouteViewModel routeView)
		{
			if (id != routeView.RequestNum)
			{
				return HttpNotFound();
			}

			// check request times
			if (routeView.DepartingTime.CompareTo(routeView.ArrivalTime) > 0)
			{
				// return error if departing time is later than arrival time
				ViewBag.AddRideErrorMessage = "Departing time cannot be later than arrival time.";
				return RedirectToAction(nameof(Index), "User");
			}

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

			// look for a dropoff location marker
			Geolocation dropOffLocation = _dbContext.Geolocations.FirstOrDefault(g => g.Description == routeView.DestinationName);
			// if none exists, make one with the new dropoff location
			if (dropOffLocation == null)
			{
				dropOffLocation = createDestinationGeolocation(routeView.DestinationName);

				// save changes
				_dbContext.Geolocations.Add(dropOffLocation);

				try
				{
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}

			// get the existing ride request
			Route route = _dbContext.Routes
				.Include("Schedule")
				.FirstOrDefault(r => r.RouteNum == routeView.RequestNum);
			// get the schedule for the ride request
			Schedule rideSchedule = route.Schedule;

			// if none exists, make a new one
			if (route == null)
			{
				route = new Route()
				{
					DriverId = userId,
					Driver = user,
					RouteNum = routeView.RequestNum,
					ArriveTime = routeView.ArrivalTime,
					DepartTime = routeView.DepartingTime,
					MaxSeatCount = routeView.MaxSeatCount,
					Geolocation = dropOffLocation
				};

				int nextScheduleId = 1;
				Schedule lastSchedule = _dbContext.Schedules.OrderByDescending(s => s.ScheduleId).FirstOrDefault();
				if (lastSchedule != null) nextScheduleId = lastSchedule.ScheduleId + 1;

				rideSchedule = new Schedule
				{
					ScheduleId = nextScheduleId,
					CheckedSunday = routeView.CheckedSunday == "on" ? true : false,
					CheckedMonday = routeView.CheckedMonday == "on" ? true : false,
					CheckedTuesday = routeView.CheckedTuesday == "on" ? true : false,
					CheckedWednesday = routeView.CheckedWednesday == "on" ? true : false,
					CheckedThursday = routeView.CheckedThursday == "on" ? true : false,
					CheckedFriday = routeView.CheckedFriday == "on" ? true : false,
					CheckedSaturday = routeView.CheckedSaturday == "on" ? true : false,
				};

				try
				{
					_dbContext.Schedules.Add(rideSchedule);
					_dbContext.SaveChanges();

					_dbContext.Routes.Add(route);
					_dbContext.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					return View("Error");
				}
			}
			// update the existing ride request
			else
			{
				// if null schedule, make a new one
				if (rideSchedule == null)
				{
					rideSchedule = new Schedule
					{
						ScheduleId = rideSchedule.ScheduleId
					};

					try
					{
						_dbContext.Schedules.Add(rideSchedule);
						_dbContext.SaveChanges();
					}
					catch
					{
						return View("Error");
					}
				}

				// update the schedule
				rideSchedule.CheckedSunday = routeView.CheckedSunday == "on" ? true : false;
				rideSchedule.CheckedMonday = routeView.CheckedMonday == "on" ? true : false;
				rideSchedule.CheckedTuesday = routeView.CheckedTuesday == "on" ? true : false;
				rideSchedule.CheckedWednesday = routeView.CheckedWednesday == "on" ? true : false;
				rideSchedule.CheckedThursday = routeView.CheckedThursday == "on" ? true : false;
				rideSchedule.CheckedFriday = routeView.CheckedFriday == "on" ? true : false;
				rideSchedule.CheckedSaturday = routeView.CheckedSaturday == "on" ? true : false;
				_dbContext.SaveChanges();
			}

			//try
			//{

			//
			// update the ride request here
			//
			route.ArriveTime = routeView.ArrivalTime;
			route.DepartTime = routeView.DepartingTime;
			route.DestinationId = dropOffLocation.LocationId;
			route.Geolocation = dropOffLocation;
			route.ScheduleId = rideSchedule.ScheduleId;
			route.Schedule = rideSchedule;
			route.MaxSeatCount = routeView.MaxSeatCount;
			_dbContext.SaveChanges();

			//}
			//catch
			//{
			//	ViewBag.AddRideErrorMessage = "Could not parse provided time correctly.";
			//	return RedirectToAction(nameof(Index), "User");
			//}

			if (route == null)
			{
				ViewBag.AddRideErrorMessage = "Could not add the desired ride request. Please try again.";
				return RedirectToAction(nameof(Index), "User");
			}

			return RedirectToAction(nameof(Index), "User");
		}

		[HttpPost]
		public ActionResult RemoveRoute(int requestNum)
		{
			// select user
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);

			// find the request in the db
			Route selection = _dbContext.Routes.Where(rr => rr.DriverId == userId).FirstOrDefault(rr => rr.RouteNum == requestNum);

			if (selection != null)
			{
				// remove selection's schedule
				Schedule schedule = selection.Schedule;

				// remove the selection if exists
				_dbContext.Routes.Remove(selection);
				_dbContext.SaveChanges();

				_dbContext.Schedules.Remove(schedule);
				_dbContext.SaveChanges();
			}

			// return to the rider home page
			return RedirectToAction(nameof(Index));
		}

		[NonAction]
		private ICollection<RouteViewModel> buildRouteViewModelList(IEnumerable<Route> routes)
		{
			// get the current user
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers
				.FirstOrDefault(u => u.Id == userId);

			if (user == null)
			{
				throw new Exception("Current user not found in database.");
			}

			// generate a lookup to use to select geolocation names
			var geolocationLookup = _dbContext.Geolocations.ToDictionary(g => g.LocationId, g => g.Description);
			IList<RouteViewModel> viewModels = new List<RouteViewModel>();

			// for each ride request supplied,
			foreach (Route rr in routes)
			{
				// create a view model
				RouteViewModel model = new RouteViewModel
				{
					//RiderId = currentUser.Id,
					RequestNum = rr.RouteNum,
					ArrivalTime = rr.ArriveTime,
					DepartingTime = rr.DepartTime,
					CheckedSunday = rr.Schedule.CheckedSunday ? "on" : null,
					CheckedMonday = rr.Schedule.CheckedMonday ? "on" : null,
					CheckedTuesday = rr.Schedule.CheckedTuesday ? "on" : null,
					CheckedWednesday = rr.Schedule.CheckedWednesday ? "on" : null,
					CheckedThursday = rr.Schedule.CheckedTuesday ? "on" : null,
					CheckedFriday = rr.Schedule.CheckedFriday ? "on" : null,
					CheckedSaturday = rr.Schedule.CheckedSaturday ? "on" : null,
					MaxSeatCount = rr.MaxSeatCount
				};

				// get drop off name from the lookup
				if (rr.DestinationId != null)
					model.DestinationName = geolocationLookup[rr.DestinationId.Value];
				else
					model.DestinationName = "";

				// add it to the list
				viewModels.Add(model);
			}

			return viewModels;
		}

		[NonAction]
		private RouteViewModel buildRouteViewModel(Route route)
		{
			return buildRouteViewModelList(new Route[1] { route }).ElementAt(0);
		}
		#endregion

		#region NONACTIONS
		[NonAction]
		private UserSchedulesViewModel generateIndexViewModel()
		{
			string userId = User.Identity.GetUserId();
			AspNetUser user = _dbContext.AspNetUsers
				.FirstOrDefault(u => u.Id == userId);

			return new UserSchedulesViewModel()
			{
				RideRequests = buildRideRequestViewModelList(user.RideRequests),
				Routes = buildRouteViewModelList(user.RoutesDriving),
				Username = user.UserName,
				FirstName = (string.IsNullOrEmpty(user.FirstName) ? "John" : user.FirstName),
				LastName = (string.IsNullOrEmpty(user.LastName) ? "Smith" : user.LastName),
				Gender = (string.IsNullOrEmpty(user.Gender) ? "None" : user.Gender.Trim()),
				IsDriver = (user.IsDriver ?? false)
			};
		}

		[NonAction]
		private Dictionary<int, string> buildGeolocationDictionary()
		{
			IEnumerable<Geolocation> geolocations = _dbContext.Routes
				.Include("Geolocation")
				.Select(r => r.Geolocation);

			Dictionary<int, string> geolocationPairs = new Dictionary<int, string>();

			foreach (var g in geolocations)
			{
				if (!geolocationPairs.ContainsKey(g.LocationId))
                geolocationPairs.Add(g.LocationId, g.Description);
            }

			return geolocationPairs;
		}

		//[NonAction]
		//private void removeUnusedGeolocations()
		//{
		//	IEnumerable<int> routeGeolocationIds = _dbContext.Routes
		//		.Where(r => r.DestinationId != null)
		//		.Select(r => r.DestinationId.Value);

		//	IEnumerable<int> dropoffGeolocationIds = _dbContext.RideRequests
		//		.Where(rr => rr.DropoffLocationId != null)
		//		.Select(rr => rr.DropoffLocationId.Value);

		//	IEnumerable<int> pickupGeolocationIds = _dbContext.RideRequests
		//		.Where(rr => rr.PickupLocationId != null)
		//		.Select(rr => rr.PickupLocationId.Value);

		//	List<int> usedGeolocationIds = new List<int>();

		//	foreach (int id in routeGeolocationIds) usedGeolocationIds.Add(id);
		//	foreach (int id in dropoffGeolocationIds) usedGeolocationIds.Add(id);
		//	foreach (int id in pickupGeolocationIds) usedGeolocationIds.Add(id);

		//	// delete the geolocations not in this list
		//	// ignore ones with the @ symbol

		//	_dbContext.Geolocations.
		//}

		[NonAction]
		private Geolocation createDestinationGeolocation(string name, decimal? longitude = null, decimal? latitude = null)
		{
			int geolocationId = 1;
			Geolocation lastMarker = _dbContext.Geolocations.OrderByDescending(g => g.LocationId).FirstOrDefault();
			if (lastMarker != null) geolocationId = lastMarker.LocationId + 1;

			return new Geolocation
			{
				LocationId = geolocationId,
				Description = (name ?? ""),
				Color = "#ff5050",
				Latitude = latitude,
				Longitude = longitude
			};
		}

		[NonAction]
		private string getUserFullName()
		{
			string userId = User.Identity.GetUserId();
			AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == userId);
			
			string firstName = "";
			string lastName = "";

			if (currentUser != null) firstName = currentUser.FirstName;
			else firstName = "Justin";

			if (currentUser != null) lastName = currentUser.LastName;
			else lastName = "Smith";

			return string.Format("{0}, {1}", lastName, firstName);
		}
		#endregion
	}
}