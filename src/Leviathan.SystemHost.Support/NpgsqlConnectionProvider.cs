using Leviathan.Data;
using Npgsql;
using System.Data.Common;
using System.Data;

namespace Leviathan.SystemHost.Support {
	public class NpgsqlConnectionProvider(NpgSqlConnectionProviderConfig config) : IConnectionProvider<NpgsqlConnection>, IConnectionProvider<DbConnection>, IConnectionProvider<IDbConnection> {

		public NpgsqlConnection CreateConnection(string? name = null) {
			var connectionString = config.ConnectionStrings[name ?? "default"];
			return connectionString == null
			   ? throw new InvalidOperationException($"Connection string '{name}' not found.")
			   : new NpgsqlConnection(connectionString);
		}

		IDbConnection IConnectionProvider<IDbConnection>.CreateConnection(string? name) => CreateConnection(name);
		DbConnection IConnectionProvider<DbConnection>.CreateConnection(string? name) => CreateConnection(name);
	}

	public class NpgSqlConnectionProviderConfig {
		public Dictionary<string, string> ConnectionStrings { get; set; } = new();
	}
}