using Npgsql;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlConnectionProvider : IDbConnectionProvider<NpgsqlConnection> {
		string connectionString;
		public NpgsqlConnectionProvider(string connectionString) {
			this.connectionString = connectionString;
		}

		public NpgsqlConnection Connect() => new(connectionString);
	}
}