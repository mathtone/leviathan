using System.Data;

namespace Leviathan.DataAccess {
	public interface IDbConnectionProvider {
		IDbConnection CreateConnection();
		IDbConnection CreateConnection(string dbName);
	}
	public interface IDbConnectionProvider<CONN> : IDbConnectionProvider where CONN : IDbConnection {
		new CONN CreateConnection();
	}

	public class DbConnectionProvider<CONN> : IDbConnectionProvider<CONN> where CONN : IDbConnection, new() {
		protected string connectionString;

		public DbConnectionProvider(string connectionString) {
			this.connectionString = connectionString;
		}

		public CONN CreateConnection() => new() {
			ConnectionString = connectionString
		};
		public CONN CreateConnection(string dbName) => CreateConnection($"{connectionString}Database={dbName};");


		IDbConnection IDbConnectionProvider.CreateConnection() => this.CreateConnection();
		IDbConnection IDbConnectionProvider.CreateConnection(string dbName) => this.CreateConnection(dbName);
	}
}