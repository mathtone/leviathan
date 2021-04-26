using System;
using System.Data;

namespace Leviathan.DataAccess {
	public static class IDbConnectionExtensions {

		public static IDbCommand CreateCommand(this IDbConnection connection, string commandText, CommandType type = CommandType.Text) {
			var rtn = connection.CreateCommand();
			rtn.CommandText = commandText;
			rtn.CommandType = type;
			return rtn;
		}

		public static void Using(this IDbConnection connection, Action<IDbConnection> action) {
			try {
				action(connection);
			}
			finally {
				connection.Dispose();
			}
		}
		public static T Using<T>(this IDbConnection connection, Func<IDbConnection, T> resultSelector) {
			try {
				connection.Open();
				return resultSelector(connection);
			}
			finally {
				connection.Dispose();
			}
		}
	}
}