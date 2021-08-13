using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Leviathan.DataAccess {
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
