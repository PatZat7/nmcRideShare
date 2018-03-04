using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NMCDriveShare.Controllers
{
    public class PortalController : Controller
    {
        // Users display portal
        public ActionResult Index()
        {
			// Connects to database through SqlClient
			// TODO: Replace SqlClient code with Entity Frameword code

			// store markers in a JSON array
			string markers = "[\n";
			string connectionString = ConfigurationManager.ConnectionStrings["NMCDriveShareEntities3"].ConnectionString;
			SqlCommand selector = new SqlCommand("SELECT * FROM vw_UserLocations");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// bind the statement to the connection
				selector.Connection = connection;

				// open the connection
				connection.Open();

				using (SqlDataReader dataReader = selector.ExecuteReader())
				{
					// read the view one item at a time
					while (dataReader.Read())
					{
						// get fields ready for a JSON object
						string name = dataReader["firstName"] as string + dataReader["lastName"] as string;
						bool isDriver = Convert.ToBoolean(dataReader["isDriver"]);
						string description = "";

						if (isDriver)
							description = "Driver";
						else
							description = "Rider";

						// generate a JSON object for each item in the DB View
						string marker = "{\n";
						
						marker += $"'title': '{name}'";
						marker += $"'lat': '{dataReader["latitude"]}'";
						marker += $"'lng': '{dataReader["longitude"]}'";
						marker += $"'description': '{description}'";
						
						marker += "},\n";

						// add the marker to the marker array
						markers += marker;
					}
				}

				// close the connection
				connection.Close();
			}

			markers += "]";

			// send JSON off to the View
			ViewBag.markers = markers;

            return View();
        }
    }
}