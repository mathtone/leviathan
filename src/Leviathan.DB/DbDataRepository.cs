using System.Data;
using Leviathan.DataAccess;

namespace Leviathan.DB {
	public abstract class DbDataService<CN, CMD>
		where CN : IDbConnection
		where CMD : IDbCommand, new() {

		protected CN Connection { get; }

		public DbDataService(CN connection) {
			this.Connection = connection;
		}

		protected virtual CMD CreateCommand(string commandText) => new() {
			Connection = this.Connection,
			CommandText = commandText
		};
	}
	public abstract class DbDataRepository<CN, CMD, T, ID> : DbDataService<CN, CMD>, IRepository<T, ID>
		where CN : IDbConnection
		where CMD : IDbCommand, new() {

		public DbDataRepository(CN connection) : base(connection) {

		}

		public abstract T Create(T item);

		public abstract void Delete(ID itemId);

		public abstract T Read(ID id);

		public abstract T Update(T item);
	}
}