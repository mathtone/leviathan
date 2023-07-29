using System.Data.Common;

namespace Leviathan.Data.Npgsql {
	public class DbConnectionProvider<CN> : IAsyncDbConnectionProvider<CN> where CN : DbConnection, new() {

		private readonly string _connectionString;

		public DbConnectionProvider(string connectionString) =>
			_connectionString = connectionString;

		public CN GetConnection() =>
			new() {
				ConnectionString = _connectionString
			};
	}

	public interface IAsyncDbConnectionProvider<out CN> : IDbConnectionProvider<CN>
		where CN : DbConnection {
	}

	public static class AsyncDbConnectionProviderExtensions {

		public static async Task<CN> OpenAsync<CN>(this IAsyncDbConnectionProvider<CN> provider) where CN : DbConnection {
			var cn = provider.GetConnection();
			await cn.OpenAsync();
			return cn;
		}

		public static async Task UseAsync<CN>(this IAsyncDbConnectionProvider<CN> provider, Func<CN, Task> action) where CN : DbConnection {
			await using var cn = await provider.OpenAsync();
			await action(cn);
		}

		public async static Task<RTN> UseAsync<CN, RTN>(this IAsyncDbConnectionProvider<CN> provider, Func<CN, Task<RTN>> action) where CN : DbConnection {
			await using var cn = await provider.OpenAsync();
			return await action(cn);
		}
	}
}