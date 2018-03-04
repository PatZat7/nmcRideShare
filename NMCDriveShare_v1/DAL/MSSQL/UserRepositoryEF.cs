using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NMCDriveShare_v1.Models;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	public class UserRepositoryEF : DriveShareRepositoryEF<User>
	{
		public UserRepositoryEF(DriveShareEntities3 dataContext)
			: base(dataContext, dc => dc.Users) {}

		public override bool Add(User item)
		{
			if (SelectOne(item.userID) == null)
			{
				// generate a new id for the user
				item.userID = getNextId();
				base.Add(item);

				Save();

				return true;
			}
			else return false;
		}

		public override bool Delete(object id)
		{
			User selection = SelectOne(id);

			if (selection != null)
			{
				_dataSet.Remove(selection);

				Save();

				return true;
			}
			else return false;
		}

		public override User SelectOne(object id)
		{
			User selection = _dataSet.FirstOrDefault(u => u.userID == (int)id);
			return selection;
		}

		public override bool Update(User updatedItem)
		{
			User selection = SelectOne(updatedItem.userID);

			// update an existing item
			// look for item with that id in the data set
			if (selection != null)
			{
				_dataSet.Remove(selection);
				_dataSet.Add(updatedItem);

				Save();

				return true;
			}
			else return false;
		}

		private int getNextId()
		{
			User lastUser = _dataSet.OrderByDescending(u => u.userID).FirstOrDefault();

			// return last user's id
			// if null, return a base value
			return lastUser != null ? lastUser.userID : 1;
		}
	}
}