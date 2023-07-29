using Npgsql;

namespace Leviathan.Data.Npgsql {
	public class NpgsqlConnectionProvider : DbConnectionProvider<NpgsqlConnection>, INpgsqlConnectionProvider {
		public NpgsqlConnectionProvider(string connectionString) :
			base(connectionString) { }
	}
	public interface INpgsqlConnectionProvider : IAsyncDbConnectionProvider<NpgsqlConnection> {

	}
}