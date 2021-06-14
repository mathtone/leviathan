using Npgsql;
using System.Data;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlConnectionProvider : IDbConnectionProvider<NpgsqlConnection>,IDbConnectionProvider {

		string ConnectionString { get; set; }

		public NpgsqlConnectionProvider(string connectionString) {
			this.ConnectionString = connectionString;
		}

		public NpgsqlConnection Connect() =>
			new(ConnectionString);

		public void SetConnectionInfo(string connectionInfo) {
			this.ConnectionString = connectionInfo;
		}

		IDbConnection IDbConnectionProvider<IDbConnection>.Connect() => Connect();
	}
}