using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NMCDriveShare_v1.Models;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	public class RideBoardRepositoryEF : DriveShareRepositoryEF<RideBoard>
	{
		public RideBoardRepositoryEF(DriveShareEntities3 dbContext)
			: base(dbContext, dc => dc.RideBoards) {}

		public override bool Add(RideBoard item)
		{
			if (!Exists(item.boardId))
			{
				item.boardId = getNextId();
				base.Add(item);

				Save();

				return true;
			}
			else return false;
		}

		private int getNextId()
		{
			RideBoard lastRideBoard = _dataSet.OrderByDescending(rb => rb.boardId).FirstOrDefault();

			return lastRideBoard != null ? lastRideBoard.boardId : 1;
		}

		public override bool Delete(object id)
		{
			RideBoard selection = SelectOne(id);

			if (selection != null)
			{
				_dataSet.Remove(selection);

				Save();

				return true;
			}
			else return false;
		}

		public override RideBoard SelectOne(object id)
		{
			RideBoard selection = _dataSet.FirstOrDefault(rb => rb.boardId == (int)id);

			return selection;
		}

		public override bool Update(RideBoard updatedItem)
		{
			RideBoard selection = SelectOne(updatedItem.boardId);

			if (selection != null)
			{
				_dataSet.Remove(selection);
				_dataSet.Add(updatedItem);

				Save();

				return true;
			}
			else return false;
		}
	}
}