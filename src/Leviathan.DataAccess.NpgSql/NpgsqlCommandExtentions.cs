using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.DataAccess.Npgsql {

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

		public static NpgsqlCommand WithInput<T>(this NpgsqlCommand command, string name, T value, NpgsqlDbType type, int size = default) =>
			command.WithParameter(name, value, ParameterDirection.Input, type, size);


	}
}