using Leviathan.DataAccess;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Leviathan.Npgsql.Sdk {
	public abstract class NpgsqlRepository<ID, T> : DbDataRepository<NpgsqlConnection, ID, T> {

		public NpgsqlRepository(NpgsqlConnection connection) : base(connection) {
		}
	}
}