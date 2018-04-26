using NMCDriveShare_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCDriveShare_v1.Utilities
{
	public static class ScheduleFormatter
	{
		public static string DisplayDaysOfWeekString(Schedule schedule)
		{
			string displayString = "";
			bool emptyText = true;

			// add delegate to minimize if statements
			Action<string> addDay = s =>
			{
				if (emptyText)
				{
					displayString += $"{s}";
					emptyText = false;
				}
				else
				{
					displayString += $", {s}";
				}
			};

			if (schedule.CheckedSunday) { addDay("Sun"); }
			if (schedule.CheckedMonday) { addDay("Mon"); }
			if (schedule.CheckedTuesday) { addDay("Tue"); }
			if (schedule.CheckedWednesday) { addDay("Wed"); }
			if (schedule.CheckedThursday)  {addDay("Thu"); }
			if (schedule.CheckedFriday) { addDay("Fri"); }
			if (schedule.CheckedSaturday) { addDay("Sat"); }

			return displayString;
		}

		public static string FormatDateTime(TimeSpan time)
		{
			int hours = time.Hours;
			int minutes = time.Minutes;
			bool pm = false;
			string pmString = "";
			string hoursString = "";
			string minutesString = "";

			if (hours == 0)
			{
				pm = false;
				hours = 12;
			}
			else if (hours > 12)
			{
				pm = true;
				hours -= 12;
			}
			else if (hours == 12)
			{
				pm = true;
			}

			pmString = pm ? "PM" : "AM";

			//if (hours < 10)
			//{
			//	hoursString = $"0{hours}";
			//}
			//else
			//{
			//	hoursString = $"{hours}";
			//}
			hoursString = $"{hours}";

			if (minutes < 10)
			{
				minutesString = $"0{minutes}";
			}
			else
			{
				minutesString = $"{minutes}";
			}

			return $"{hoursString}:{minutesString} {pmString}";
		}
	}
}