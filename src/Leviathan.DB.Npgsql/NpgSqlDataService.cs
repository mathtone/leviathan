using Npgsql;

namespace Leviathan.DB.Npgsql {
	public class NpgSqlDataService : DbDataService<NpgsqlConnection, NpgsqlCommand> {
		public NpgSqlDataService(NpgsqlConnection connection) : base(connection) {
		}
	}
}