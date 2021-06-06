using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Leviathan.DataAccess.Npgsql {

	//public static class NpgsqlCommandExtensions {
	//	public static NpgsqlCommand WithInput(this NpgsqlCommand command) {
	//		return command;
	//	}
	//}

	public static class NpgsqlCommandExtentions {
		public static NpgsqlCommand WithParameter<T>(this NpgsqlCommand command, string name, T value, ParameterDirection direction, NpgsqlDbType type, int size = default) {

			var p = command.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			p.Direction = direction;
			p.Size = size;
			p.NpgsqlDbType = type;
			return command.WithParameter(p);
		}

		public static NpgsqlCommand WithInput<T>(this NpgsqlCommand command, string name, T value, NpgsqlDbType type, int size = default)=>
			command.WithParameter(name, value, ParameterDirection.Input, type, size);
	}

	public static class NpgsqlConnectionExtensions {

		public static NpgsqlCommand CreateCommand(this NpgsqlConnection connection, string commandText, CommandType type = CommandType.Text) {
			var rtn = connection.CreateCommand();
			rtn.CommandText = commandText;
			rtn.CommandType = type;
			return rtn;
		}

		//public static void Used<CN>(this CN connection, Action<CN> action) where CN : IDbConnection {
		//	try {
		//		connection.Open();
		//		action(connection);
		//	}
		//	finally {
		//		connection.Dispose();
		//	}
		//}
		//public static T Used<T, CN>(this CN connection, Func<CN, T> resultSelector) where CN : IDbConnection {
		//	try {
		//		connection.Open();
		//		return resultSelector(connection);
		//	}
		//	finally {
		//		connection.Dispose();
		//	}
		//}

		//public static NpgsqlCommand CreateCommand(this NpgsqlConnection connection, string commandText, CommandType type = CommandType.Text) {
		//	var rtn = connection.CreateCommand();
		//	rtn.CommandText = commandText;
		//	rtn.CommandType = type;
		//	return rtn;
		//}
	}

	//public static class NpgSqlCommandExtensions {
	//	public static NpgsqlCommand WithInput<T>(this NpgsqlCommand command, string name, T value, NpgsqlDbType type, int size = default) {
	//		//command.CreateParameter("", 0, ParameterDirection.Input);
	//		//var p = CreateParameter(command,name,value,ParameterDirection.Input,size,);

	//		return command.WithParameter(name, value, ParameterDirection.Input, size);
	//	}

	//}

	public class NpgsqlDataService : DbDataService<NpgsqlConnection, NpgsqlCommand> {
		public NpgsqlDataService(string connectionString) : base(connectionString) { }
	}

	public class NpgsqlConnectionProvider : DbConnectionProvider<NpgsqlConnection> {
		public NpgsqlConnectionProvider(string connectionString) : base(connectionString) {
		}
	}
}