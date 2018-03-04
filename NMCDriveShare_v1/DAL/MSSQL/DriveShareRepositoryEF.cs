using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NMCDriveShare_v1.Models;
using System.Data.Entity;

namespace NMCDriveShare_v1.DAL.MSSQL
{
	/// <summary>
	/// A class containing baseline functionality for a data access utility
	/// for models under the <seealso cref="DriveShareEntities3"/> context.
	/// </summary>
	/// <typeparam name="T">
	/// The model being exchanged through the repository.
	/// Note: This must be a model that can be found in a
	/// <seealso cref="DriveShareEntities3"/> context.
	/// </typeparam>
	public abstract class DriveShareRepositoryEF<T> : IRepository<T> where T:class
	{
		protected DriveShareEntities3 _dataContext;
		protected DbSet<T> _dataSet;

		/// <summary>
		/// The template for a constructor of a <seealso cref="DriveShareRepositoryEF{T}"/> repository.
		/// Instantiates a repository from a database context and a selector function.
		/// </summary>
		/// <param name="dbContext">A DriveShareEntities context for the repository to use.</param>
		/// <param name="dbSetSelector">A function that selects an appropriate dataset from <paramref name="dbContext"/>.</param>
		public DriveShareRepositoryEF(DriveShareEntities3 dbContext, Func<DriveShareEntities3, DbSet<T>> dbSetSelector)
		{
			_dataContext = dbContext;
			_dataSet = dbSetSelector(dbContext);
		}

		/// <summary>
		/// Adds a new item to the repository.
		/// By default, adds an object without checking the database for duplicates.
		/// Override recommended when <seealso cref="T"/> has a primary key.
		/// </summary>
		/// <param name="item">A record to insert in the database</param>
		/// <returns>Status indicating if the add operation occurred</returns>
		public virtual bool Add(T item)
		{
			_dataSet.Add(item);
			return true;
		}

		/// <summary>
		/// Deletes an item from the repository
		/// </summary>
		/// <param name="id">A unique identifier for the item</param>
		/// <returns>Status indicating the success of the delete</returns>
		public abstract bool Delete(object id);

		public IEnumerable<T> SelectAll()
		{
			return _dataSet.ToArray() as IEnumerable<T>;
		}

		/// <summary>
		/// Selects an item from the repository
		/// </summary>
		/// <param name="id">A unique identifier for the item</param>
		/// <returns>An item that matches the select or null if no match occurred</returns>
		public abstract T SelectOne(object id);

		/// <summary>
		/// Updates an item in the repository
		/// </summary>
		/// <param name="updatedItem">The item to update in the repository</param>
		/// <returns>Status indicating the success of the update; false if no update occurred.</returns>
		public abstract bool Update(T updatedItem);

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_dataSet = null;
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				_dataContext.Dispose();
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		~DriveShareRepositoryEF()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}
		#endregion

		/// <summary>
		/// Saves the changes to the data set.
		/// Should be called by the Add(), Delete(), and Update() methods after changes are made.
		/// </summary>
		protected void Save()
		{
			_dataContext.SaveChanges();
		}

		/// <summary>
		/// Determines if the repository contains an object matching the identifier.
		/// Uses SelectOne() to find a match.
		/// </summary>
		/// <param name="id">A unique identifier for the object</param>
		/// <returns>A boolean indicating if the object is contained in the repository</returns>
		protected virtual bool Exists(object id)
		{
			return SelectOne(id) != null;
		}
	}
}