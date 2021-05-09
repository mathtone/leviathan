using System;
using Npgsql;

namespace Leviathan.DataAccess.Npgsql {
	public class NpgsqlDataService : DbDataService<NpgsqlConnection, NpgsqlCommand> {
		public NpgsqlDataService(string connectionString) : base(connectionString) { }
	}
}