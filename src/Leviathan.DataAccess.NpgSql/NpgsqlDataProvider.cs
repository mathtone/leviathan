using Npgsql;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlDataProvider : DbDataProvider<NpgsqlConnection, NpgsqlCommand> {
		public NpgsqlDataProvider(IDbConnectionProvider<NpgsqlConnection> provider) :
			base(provider) {
		}
	}
}