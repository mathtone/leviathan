using System;
using System.Data;

namespace Leviathan.DataAccess {
	public class DataAccessComponent {
		//private bool disposedValue;
		protected IDbConnection Connection { get; }

		public DataAccessComponent(IDbConnection connection) {
			this.Connection = connection;
		}

		//protected virtual void Dispose(bool disposing) {
		//	if (!disposedValue) {
		//		if (disposing) {
		//			Connection.Dispose();
		//		}
		//		disposedValue = true;
		//	}
		//}

		//public void Dispose() {
		//	Dispose(disposing: true);
		//	GC.SuppressFinalize(this);
		//}
	}
}
