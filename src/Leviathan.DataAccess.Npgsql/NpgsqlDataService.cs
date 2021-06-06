using System;
using System.Data;
using Npgsql;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlDataService : DbDataService<NpgsqlConnection, NpgsqlCommand> {
		public NpgsqlDataService(string connectionString) : base(connectionString) { }
	}

	public class NpgsqlConnectionProvider : DbConnectionProvider<NpgsqlConnection> {
		public NpgsqlConnectionProvider(string connectionString) : base(connectionString) {
		}
	}
}