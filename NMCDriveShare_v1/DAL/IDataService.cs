using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMCDriveShare_v1.DAL
{
	public interface IDataService<T>
	{
		IEnumerable<T> InitializeData();
		IEnumerable<T> ReadItems();
		void WriteItems(IEnumerable<T> item);
	}
}
