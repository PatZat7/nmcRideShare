using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NMCDriveShare_v1.Models;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Threading.Tasks;
using NMCDriveShare_v1.Utilities;

namespace NMCDriveShare_v1.Controllers
{
	[Authorize]
	public class PortalController : Controller
    {
		private readonly DriveShareEntities3 _dbContext = new DriveShareEntities3();

		public PortalController()
		{
			//if (User != null) ViewBag.IsDriver = UserStatusChecker.CheckDriverStatus(_dbContext, User.Identity.GetUserId());
		}

		// Restored changes from a previous commit
		// Users display portal
		public ActionResult Index()
		{
			string userId = User.Identity.GetUserId();
			AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(au => au.Id == userId);

			/*
			//// Connects to database through SqlClient
			//// TODO: Replace SqlClient code with Entity Frameword code

			//// store markers in a JSON array
			//string markers = "[\n";
			//string connectionString = ConfigurationManager.ConnectionStrings["DriveShareEntities3Sql"].ConnectionString;
			//SqlCommand selector = new SqlCommand("SELECT * FROM vw_UserLocations");

			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	// bind the statement to the connection
			//	selector.Connection = connection;

			//	// open the connection
			//	connection.Open();

			//	using (SqlDataReader dataReader = selector.ExecuteReader())
			//	{
			//		// read the view one item at a time
			//		while (dataReader.Read())
			//		{
			//			// get fields ready for a JSON object
			//			string name = dataReader["firstName"] as string + dataReader["lastName"] as string;
			//			bool isDriver = Convert.ToBoolean(dataReader["isDriver"]);
			//			string description = "";

			//			if (isDriver)
			//				description = "Driver";
			//			else
			//				description = "Rider";

			//			// generate a JSON object for each item in the DB View
			//			string marker = "{\n";

			//			marker += $"'title': '{name}'";
			//			marker += $"'lat': '{dataReader["latitude"]}'";
			//			marker += $"'lng': '{dataReader["longitude"]}'";
			//			marker += $"'description': '{description}'";

			//			marker += "},\n";

			//			// add the marker to the marker array
			//			markers += marker;
			//		}
			//	}

			//	// close the connection
			//	connection.Close();
			//}

			//markers += "]";

			//// send JSON off to the View
			//ViewBag.markers = markers;
			*/

			//
			// store markers in a JSON array
			//
			string markers = "[\n";

			// select top 100 rows
			IEnumerable<vw_UserLocations> userLocations = _dbContext.vw_UserLocations.Take(100);

			foreach (vw_UserLocations v in userLocations)
			{
				string name = v.UserName;
				bool isDriver = (v.IsDriver ?? false);
				string description = "";

				if (isDriver)
					description = "Driver";
				else
					description = "Rider";

				// generate a JSON object for each item in the DB View
				string marker = "\t{\n";

				marker += $"\t\t'title': '{name}',\n";
				marker += $"\t\t'lat': '{v.Latitude}',\n";
				marker += $"\t\t'lng': '{v.Longitude}',\n";
				marker += $"\t\t'description': '{description}'\n";

				marker += "\t},\n";

				// add the marker to the marker array
				markers += marker;
			}

			markers += "]";

			ViewBag.markers = markers;

			// get currently active ride requests
			DateTime currentTime = DateTime.Now;
			IEnumerable<RideRequest> requests = _dbContext.RideRequests.Where(rr => rr.RiderId != userId).ToList();
			IEnumerable<Route> routes = _dbContext.Routes.Where(rr => rr.DriverId != userId).ToList();

			// get daily active requests
			//switch (currentTime.DayOfWeek)
			//{
			//	case DayOfWeek.Sunday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedSunday == true);
			//		break;
			//	case DayOfWeek.Monday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedMonday == true);
			//		break;
			//	case DayOfWeek.Tuesday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedTuesday == true);
			//		break;
			//	case DayOfWeek.Wednesday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedWednesday == true);
			//		break;
			//	case DayOfWeek.Thursday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedThursday == true);
			//		break;
			//	case DayOfWeek.Friday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedFriday == true);
			//		break;
			//	case DayOfWeek.Saturday:
			//		requests = requests.Where(rr => rr.Schedule.CheckedSaturday == true);
			//		break;
			//	default:
			//		break;
			//}

			// get requests to come, ignore passed ones
			// order requests with earlier times first
			//requests = requests.Where(rr => rr.DepartingTime.CompareTo(currentTime.TimeOfDay) > 0)
			//	.OrderBy(rr => rr.DepartingTime);

			ViewBag.ActiveRideRequests = requests;
			ViewBag.ActiveRoutes = routes;
			ViewBag.IsDriver = currentUser.IsDriver;

			return View();
		}
		
		[HttpPost]
		public ActionResult UpdateUserLocation(decimal? latitude, decimal? longitude)
		{
			try
			{
				// find a user record with corresponding identity
				string userId = User.Identity.GetUserId();
				AspNetUser currentUser = _dbContext.AspNetUsers.FirstOrDefault(au => au.Id == userId);
				// make a copy of the user
				//AspNetUser currentUserTemp = new AspNetUser()
				//{
				//	AccessFailedCount = currentUser.AccessFailedCount, Age = currentUser.Age, AspNetRoles = currentUser.AspNetRoles,
				//	AspNetUserClaims = currentUser.AspNetUserClaims, AspNetUserLogins = currentUser.AspNetUserLogins,
				//	ChatMessagesStartedBy = currentUser.ChatMessagesStartedBy, ChatMessagesStartedTo = currentUser.ChatMessagesStartedTo,
				//	ChatThreadsFrom = currentUser.ChatThreadsFrom, ChatThreadsTo = currentUser.ChatThreadsTo, Email = currentUser.Email,
				//	EmailConfirmed = currentUser.EmailConfirmed, FirstName = currentUser.FirstName, Gender = currentUser.Gender,
				//	Geolocation = currentUser.Geolocation, Id = currentUser.Id, IsActive = currentUser.IsActive, IsDriver = currentUser.IsDriver,
				//	LastName = currentUser.LastName, LocationId = currentUser.LocationId, LockoutEnabled = currentUser.LockoutEnabled,
				//	LockoutEndDateUtc = currentUser.LockoutEndDateUtc, PasswordHash = currentUser.PasswordHash, PhoneNumber = currentUser.PhoneNumber,
				//	PhoneNumberConfirmed = currentUser.PhoneNumberConfirmed, ProfilePictures = currentUser.ProfilePictures,
				//	RideRequests = currentUser.RideRequests, RoutesDriving = currentUser.RoutesDriving, RoutesRiding = currentUser.RoutesRiding,
				//	SecurityStamp = currentUser.SecurityStamp, TwoFactorEnabled = currentUser.TwoFactorEnabled, UserName = currentUser.UserName
				//};

				string statusString = "";

				// create a new Geolocation for the user
				if (currentUser.LocationId == null)
				{
					// if user has no location and geolocation fails, do not add a location
					// else add a location
					if (latitude.HasValue && longitude.HasValue)
					{
						// generate a new id for the geolocation
						int locationId = (_dbContext.Geolocations.OrderByDescending(g => g.LocationId)
							.FirstOrDefault() ?? new Geolocation { LocationId = 0 }).LocationId + 1;

						// create a new Geolocation
						Geolocation userLocation = new Geolocation
						{
							LocationId = locationId,
							Latitude = latitude.Value,
							Longitude = longitude.Value,
							// color depends on whether the user is a rider or driver
							// red if driver and blue if rider
							Color = (currentUser.IsDriver ?? false) ? "#ff5050" : "#0066ff",
							Description = ($"{currentUser.UserName} (" + ((currentUser.IsDriver ?? false) ? "Driver" : "Rider") + ")")
						};

						// add the geolocation to the database
						_dbContext.Geolocations.Add(userLocation);
						_dbContext.SaveChanges();

						// update the current user
						// 1) remove the user in the database
						//AspNetUser selectedUser = _dbContext.AspNetUsers.FirstOrDefault(au => au.Id == currentUser.Id);
						//_dbContext.AspNetUsers.Remove(selectedUser);
						//_dbContext.SaveChanges();

						// attach the geolocation to the current user
						currentUser.LocationId = userLocation.LocationId;
						currentUser.Geolocation = userLocation;

						// 2) re-insert the user
						//_dbContext.AspNetUsers.Add(currentUser);
						_dbContext.SaveChanges();

						userLocation = _dbContext.Geolocations.FirstOrDefault(g => g.LocationId == currentUser.LocationId);

						statusString = $"Added location for user \"{currentUser.UserName}\"." +
							$"Location is ({userLocation.Latitude}, {userLocation.Longitude}).";
					}
					else
					{
						statusString = $"No changes made.";
					}
				}
				// if user has an existing geolocation, update it or delete it
				else
				{
					// if user has an existing location and geolocation persists, update the user's location
					if (latitude.HasValue && longitude.HasValue)
					{
						// find the user's existing location in the database
						Geolocation currentLocation = _dbContext.Geolocations.FirstOrDefault(g => g.LocationId == currentUser.LocationId);

						// update the geolocation with the geolocation information
						currentLocation.Latitude = latitude;
						currentLocation.Longitude = longitude;

						// update the geolocation in the database
						//Geolocation selectedLocation = _dbContext.Geolocations.FirstOrDefault(g => g.LocationId == currentLocation.LocationId);
						//_dbContext.Geolocations.Remove(selectedLocation);
						//_dbContext.SaveChanges();
						//_dbContext.Geolocations.Add(currentLocation);
						//_dbContext.SaveChanges();

						_dbContext.SaveChanges();

						// attach the updated location to the user
						currentUser.Geolocation = currentLocation;
						currentUser.LocationId = currentLocation.LocationId;

						// update the current user
						// remove the user in the database and add the one from memory
						//AspNetUser selectedUser = _dbContext.AspNetUsers.FirstOrDefault(au => au.Id == currentUser.Id);
						//_dbContext.AspNetUsers.Remove(selectedUser);
						//_dbContext.SaveChanges();
						//_dbContext.AspNetUsers.Add(currentUser);
						//_dbContext.SaveChanges();

						_dbContext.SaveChanges();

						Geolocation userLocation = _dbContext.Geolocations.FirstOrDefault(g => g.LocationId == currentUser.LocationId);

						statusString = $"Updated location for user \"{currentUser.UserName}\". Location is ({userLocation.Latitude}, {userLocation.Longitude}).";

					}
					// else, if geolocation fails, remove it
					else
					{
						// find the existing Geolocation object
						Geolocation currentLocation = _dbContext.Geolocations.FirstOrDefault(g => g.LocationId == currentUser.LocationId);

						// detach the location from the user
						currentUser.LocationId = null;
						currentUser.Geolocation = null;

						// update the current user
						// remove the user in the database and add the one from memory
						//AspNetUser selectedUser = _dbContext.AspNetUsers.FirstOrDefault(au => au.Id == currentUser.Id);
						//_dbContext.AspNetUsers.Remove(selectedUser);
						//_dbContext.SaveChanges();
						//_dbContext.AspNetUsers.Add(currentUser);
						_dbContext.SaveChanges();

						// remove it from the db context
						_dbContext.Geolocations.Remove(currentLocation);
						_dbContext.SaveChanges();

						statusString = $"Removed location for user \"{currentUser.UserName}\".";
					}
				}


				return Json(new { result = statusString }, JsonRequestBehavior.AllowGet);
			}
			catch (MetadataException)
			{
				return Json(new { result = $"An error occurred while loading the database." }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}