using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NMCDriveShare_v1.Models;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	// NOTE: Commented out due to having no way to modify a model's properties
	// during insert

	//public class DefaultRepositoryEF<T, PK> : DriveShareRepositoryEF<T>
	//	where T : class
	//	where PK : IEquatable<PK>
	//{
	//	protected Func<T, PK> _primaryKeySelector;

	//	public Func<T, PK> PrimaryKeySelector => _primaryKeySelector;

	//	public DefaultRepositoryEF(DriveShareEntities3 dataContext, Func<DriveShareEntities3, DbSet<T>> dbSetSelector,
	//		Func<T, PK> pkSelector)
	//		: base(dataContext, dbSetSelector)
	//	{
	//		_primaryKeySelector = pkSelector;
	//	}

	//	public override bool Add(T item)
	//	{
	//		if (SelectOne(_primaryKeySelector(item)) != null)
	//		{

	//			return true;
	//		}
	//		else return false;
	//	}

	//	public override bool Delete(object id)
	//	{
			
	//	}

	//	public override T SelectOne(object id)
	//	{
	//		// uses the PK selector to search for an object and compares it to the given value
	//		T selection = _dataSet.FirstOrDefault(t => _primaryKeySelector(t).Equals((PK)id));
	//		return selection;
	//	}

	//	public virtual T SelectOne(PK id)
	//	{
	//		return SelectOne(id);
	//	}

	//	public override bool Update(T updatedItem)
	//	{
			
	//	}
	//}
}