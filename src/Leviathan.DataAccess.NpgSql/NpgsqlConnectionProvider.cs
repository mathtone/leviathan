using Npgsql;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlConnectionProvider : IDbConnectionProvider<NpgsqlConnection> {

		readonly string connectionString;

		public NpgsqlConnectionProvider(string connectionString) {
			this.connectionString = connectionString;
		}

		public NpgsqlConnection Connect(string ) => new(connectionString);
	}
}