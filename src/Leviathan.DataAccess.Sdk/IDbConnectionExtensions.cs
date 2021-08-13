using System;
using System.Data;
using System.Text;

namespace Leviathan.DataAccess {
	public static class IDbConnectionExtensions {

		public static CMD CreateCommand<CN, CMD>(this CN connection, string commandText, CommandType type = CommandType.Text)
			where CN : IDbConnection
			where CMD : IDbCommand {

			var rtn = connection.CreateCommand();
			rtn.CommandText = commandText;
			rtn.CommandType = type;
			return (CMD)rtn;
		}

		public static void Used<CN>(this CN connection, Action<CN> action) where CN : IDbConnection {
			try {
				connection.Open();
				action(connection);
			}
			finally {
				connection.Close();
				connection.Dispose();
			}
		}

		public static T Used<T, CN>(this CN connection, Func<CN, T> resultSelector) where CN : IDbConnection {
			try {
				connection.Open();
				return resultSelector(connection);
			}
			finally {
				connection.Close();
				connection.Dispose();
			}
		}
	}
}
