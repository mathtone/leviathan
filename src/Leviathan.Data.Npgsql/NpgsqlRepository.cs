using Mathtone.Sdk.Patterns;
using Npgsql;
using System.Data;
using Mathtone.Sdk.Data;
namespace Leviathan.Data.Npgsql {

	public abstract class NpgsqlRepository<ID, ITEM> : IAsyncListRepository<ID, ITEM> {

		protected IConnectionProvider<NpgsqlConnection> Connections { get; }

		protected NpgsqlRepository(IConnectionProvider<NpgsqlConnection> connections) =>
			Connections = connections;

		public async ValueTask<ID> Create(ITEM item) =>
			(ID)(await CreateCommand(await Connections.OpenAsync(), item)!
				.ExecuteScalarAsync()
			)!;

		public async Task Delete(ID id) => await DeleteCommand(await Connections.OpenAsync(), id)!
				.ExecuteNonQueryAsync();

		public async ValueTask<ITEM?> Read(ID id) {

			var rtn = await (ReadCommand(await Connections.OpenAsync(), id))
				.ExecuteReaderAsync()
				.ConsumeAsync(CreateItem)
				.SingleOrDefaultAsync();
			return rtn;
		}

		public async IAsyncEnumerable<ITEM> ReadAll() {
			await using var cn = await Connections.OpenAsync();
			var rtn = ReadAllCommand(cn)
				.ExecuteReaderAsync()
				.ConsumeAsync(CreateItem);

			await foreach (var r in rtn)
				yield return r;
		}

		public Task Update(ITEM item) {
			throw new NotImplementedException();
		}

		protected abstract ITEM CreateItem(IDataRecord r);
		protected abstract NpgsqlCommand ReadAllCommand(NpgsqlConnection cn);
		protected abstract NpgsqlCommand ReadCommand(NpgsqlConnection cn, ID id);
		protected abstract NpgsqlCommand CreateCommand(NpgsqlConnection cn, ITEM item);
		protected abstract NpgsqlCommand UpdateCommand(NpgsqlConnection cn, ITEM item);
		protected abstract NpgsqlCommand DeleteCommand(NpgsqlConnection cn, ID id);
	}
}
