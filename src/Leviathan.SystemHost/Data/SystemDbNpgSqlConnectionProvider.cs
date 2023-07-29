using Leviathan.Data.Npgsql;

namespace Leviathan.SystemHost.Data {
	public class SystemDbNpgSqlConnectionProvider : NpgsqlConnectionProvider, ISystemDbConnectionProvider {
		public SystemDbNpgSqlConnectionProvider(string connectionString) :
			base(connectionString) {
		}
	}
}