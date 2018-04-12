using NMCDriveShare_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Utilities
{
	public static class UserStatusChecker
	{
		public static bool? CheckDriverStatus(DriveShareEntities3 dbContext, string userId)
		{
			AspNetUser user = dbContext.AspNetUsers.FirstOrDefault(au => au.Id == userId);

			if (user == null) return null;
			else return (user.IsDriver ?? false);
		}
	}
}