
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Leviathan.Data {
	public interface IConnectionProvider<out CN> {
		CN CreateConnection(string? name = default);
	}

	public static class ConnectionProviderAsyncExtensions {
		public static async Task<CN> OpenAsync<CN>(this IConnectionProvider<CN> provider, string? name = default, CancellationToken cancel = default)
		   where CN : DbConnection {

			var cn = provider.CreateConnection(name);
			await cn.OpenAsync(cancel);
			return cn;
		}

		public static async Task ExecuteAsync<CN, CMD>(this IConnectionProvider<CN> provider, string? name, Func<CN, CMD> createCommand, Func<CMD, Task> action, CancellationToken cancellation = default)
		  where CN : DbConnection
		  where CMD : DbCommand =>
			 await provider.ExecuteAsync(name, cn => action(createCommand(cn)), cancellation);

		public static async Task ExecuteAsync<CN>(this IConnectionProvider<CN> provider, string? name, Func<CN, Task> action, CancellationToken cancellation = default)
		   where CN : DbConnection {

			await using var cn = await provider.OpenAsync(name, cancellation);
			await action(cn);
			await cn.CloseAsync().ConfigureAwait(false); //maybe?
		}

		public static async Task<T> ExecuteAsync<CN, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, Task<T>> action, CancellationToken cancellation = default)
		   where CN : DbConnection =>
		   await provider.ExecuteAsync(name, cn => action(cn), cancellation);

		public static async Task<T> ExecuteAsync<CN, CMD, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, CMD> createCommand, Func<CMD, Task<T>> action, CancellationToken cancellation = default)
		   where CN : DbConnection
		   where CMD : DbCommand {

			await using var cn = await provider.OpenAsync(name, cancel: cancellation);
			await using var cmd = createCommand(cn);
			try {
				return await action(cmd);
			}
			finally {
				await cn.CloseAsync();
			}
		}

		public static IAsyncEnumerable<T> ConsumeAsync<CN, CMD, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, CMD> createCommand, Func<CMD, IAsyncEnumerable<T>> action, [EnumeratorCancellation] CancellationToken cancellation = default)
		   where CN : DbConnection
		   where CMD : DbCommand =>
			  provider.ConsumeAsync(name, cn => action(createCommand(cn)), cancellation);

		public static async IAsyncEnumerable<T> ConsumeAsync<CN, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, IAsyncEnumerable<T>> action, [EnumeratorCancellation] CancellationToken cancellation = default)
		   where CN : DbConnection {

			await using var cn = await provider.OpenAsync(name, cancellation);

			try {
				await foreach (var item in action(cn))
					yield return item;
			}
			finally {
				await cn.CloseAsync();
			}
		}
	}
}
