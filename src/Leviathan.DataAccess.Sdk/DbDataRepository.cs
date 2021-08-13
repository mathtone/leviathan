using Leviathan.Common;
using System.Data;

namespace Leviathan.DataAccess {
	public abstract class DbDataRepository<CN, ID, T> : AsyncRepository<ID, T>
		where CN : IDbConnection {

		protected CN Connection { get; }

		public DbDataRepository(CN connection) {
			Connection = connection;
		}
	}
}