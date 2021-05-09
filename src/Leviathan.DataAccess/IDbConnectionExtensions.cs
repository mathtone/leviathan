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

		public static void Used<CN>(this CN connection, Action<CN> action) where CN : IDbConnection {
			try {
				connection.Open();
				action(connection);
			}
			finally {
				connection.Dispose();
			}
		}
		public static T Used<T, CN>(this CN connection, Func<CN, T> resultSelector) where CN : IDbConnection {
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