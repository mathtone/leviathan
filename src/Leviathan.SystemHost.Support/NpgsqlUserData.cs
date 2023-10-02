using Leviathan.Data;
using Leviathan.Users;
using Npgsql;
using Mathtone.Sdk.Data;
using Mathtone.Sdk.Data.Npgsql;
using System.Data;
using Leviathan.Data.Npgsql;

namespace Leviathan.SystemHost.Support {

	public class NpgsqlUserData : NpgsqlRepository<int, UserRecord>, IUserData {
		
		const string _tableName = "core.login";

		public NpgsqlUserData(IConnectionProvider<NpgsqlConnection> connections) :
			base(connections) { }

		protected override NpgsqlCommand ReadAllCommand(NpgsqlConnection cn) =>
			cn.TextCommand($"SELECT * FROM {_tableName}");

		protected override NpgsqlCommand ReadCommand(NpgsqlConnection cn, int id) => cn
			.TextCommand($"SELECT * FROM {_tableName} WHERE id = @id")
			.WithInput("@id", id);

		protected override NpgsqlCommand UpdateCommand(NpgsqlConnection cn, UserRecord item) {
			throw new NotImplementedException();
		}

		protected override NpgsqlCommand DeleteCommand(NpgsqlConnection cn, int id) => cn
			.TextCommand($"DELETE FROM {_tableName} WHERE id = @id")
			.WithInput("@id", id);

		public async Task<UserRecord?> ReadByLogin(string login) {
			await using var cn = await Connections.OpenAsync();
			return await cn.TextCommand($"SELECT * FROM {_tableName} WHERE login = @name")
				.WithInput("@name", login)
				.ExecuteReaderAsync()
				.ConsumeAsync(CreateItem)
				.SingleOrDefaultAsync();
		}

		protected override UserRecord CreateItem(IDataRecord r) => new(
			r.Field<long>("id"),
			r.Field<string>("login")!
		) {
			PasswordHash = r.Field<string>("auth_text")!
		};

		protected override NpgsqlCommand CreateCommand(NpgsqlConnection cn, UserRecord item) => cn
			.TextCommand($"INSERT INTO {_tableName} (login, auth_text) VALUES (@login, @auth_text) RETURNING id")
			.WithInput("@login", item.Login)
			.WithInput("@auth_text", item.PasswordHash);
	}


}