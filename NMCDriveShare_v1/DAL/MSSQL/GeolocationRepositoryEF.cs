using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NMCDriveShare_v1.Models;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	public class GeolocationRepositoryEF : DriveShareRepositoryEF<Geolocation>
	{
		public GeolocationRepositoryEF(DriveShareEntities3 dataContext)
			: base(dataContext, dc => dc.Geolocations) { }

		public override bool Add(Geolocation item)
		{
			if (SelectOne(item.locationId) == null)
			{
				item.locationId = getNextId();
				base.Add(item);

				Save();

				return true;
			}
			else return false;
		}

		public override bool Delete(object id)
		{
			Geolocation selection = SelectOne(id);

			if (selection != null)
			{
				_dataSet.Remove(selection);

				Save();

				return true;
			}
			else return false;
		}

		public override Geolocation SelectOne(object id)
		{
			Geolocation location = _dataSet.FirstOrDefault(l => l.locationId == (int)id);
			return location;
		}

		public override bool Update(Geolocation updatedItem)
		{
			Geolocation location = SelectOne(updatedItem.locationId);

			if (location != null)
			{
				_dataSet.Remove(location);
				_dataSet.Add(updatedItem);

				Save();

				return true;
			}
			else return false;
		}

		private int getNextId()
		{
			Geolocation lastLocation = _dataSet.OrderByDescending(l => l.locationId).FirstOrDefault();

			return lastLocation != null ? lastLocation.locationId : 1;
		}
	}
}