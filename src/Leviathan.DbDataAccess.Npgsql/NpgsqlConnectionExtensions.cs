using Npgsql;
using System.Data;

namespace Leviathan.DbDataAccess.Npgsql {
	public static class NpgsqlConnectionExtensions {

		public static NpgsqlCommand CreateCommand(this NpgsqlConnection connection, string commandText, CommandType type = CommandType.Text) {
			var rtn = connection.CreateCommand();
			rtn.CommandText = commandText;
			rtn.CommandType = type;
			return rtn;
		}
	}
}