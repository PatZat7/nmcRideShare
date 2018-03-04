using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMCDriveShare_v1.DAL
{
	public interface IRepository<T> : IDisposable
	{
		IEnumerable<T> SelectAll();
		T SelectOne(object id);
		bool Add(T item);
		bool Delete(object id);
		bool Update(T updatedItem);
	}
}
