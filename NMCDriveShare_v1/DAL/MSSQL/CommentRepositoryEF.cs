using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NMCDriveShare_v1.Models;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	public class CommentRepositoryEF : DriveShareRepositoryEF<Comment>
	{
		public CommentRepositoryEF(DriveShareEntities3 dataContext)
			: base(dataContext, dc => dc.Comments) {}

		public override bool Add(Comment item)
		{
			if (!Exists(item.commentID))
			{
				item.commentID = getNextId();
				base.Add(item);

				Save();

				return true;
			}
			else return false;
		}

		private int getNextId()
		{
			Comment lastComment = _dataSet.OrderByDescending(c => c.commentID).FirstOrDefault();
			return lastComment.commentID;
		}

		public override bool Delete(object id)
		{
			Comment selection = SelectOne(id);

			if (selection != null)
			{
				_dataSet.Remove(selection);

				Save();

				return true;
			}
			else return false;
		}

		public override Comment SelectOne(object id)
		{
			Comment selection = _dataSet.FirstOrDefault(c => c.commentID == (int)id);
			return selection;
		}

		public override bool Update(Comment updatedItem)
		{
			Comment selection = SelectOne(updatedItem.commentID);

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