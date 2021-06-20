using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Leviathan.DbDataAccess {

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

	public static class DbConnectionExtensions {
		public static async Task<T> UsedAsync<CN, T>(this CN connection, Func<CN, Task<T>> selectorTask) where CN : DbConnection {
			try {
				//can that be right?
				return await await connection
					.OpenAsync()
					.ContinueWith(t => selectorTask(connection));
			}
			finally {
				await connection.CloseAsync().ContinueWith(t => connection.DisposeAsync());
			}
		}

		public static async Task UsedAsync<CN>(this CN connection, Func<CN, Task> actionTask) where CN : DbConnection {
			try {
				await connection.OpenAsync();
				await actionTask(connection);
			}
			finally {
				await connection.CloseAsync().ContinueWith(t => connection.DisposeAsync());
			}
		}
	}
}