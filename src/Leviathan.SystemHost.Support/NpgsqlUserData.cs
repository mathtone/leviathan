using Leviathan.Data;
using Leviathan.Users;
using Npgsql;
using Mathtone.Sdk.Data;
using Mathtone.Sdk.Data.Npgsql;
using System.Data;

namespace Leviathan.SystemHost.Support {
	public class NpgsqlUserData(IConnectionProvider<NpgsqlConnection> connections) : IUserData {

		public async ValueTask<int> Create(UserRecord item) {
			await using var cn = await connections.OpenAsync();
			var rec = await cn
				.TextCommand("INSERT INTO auth.login (login,auth_text) VALUES(@name,@pwd_hash) RETURNING id")
				.WithInput("@name", item.Login)
				.WithInput("@pwd_hash", item.PasswordHash)
				.ExecuteScalarAsync();
			return (int)rec!;
		}

		public Task Delete(int id) {
			throw new NotImplementedException();
		}

		public ValueTask<UserRecord> Read(int id) {
			throw new NotImplementedException();
		}

		public IAsyncEnumerable<UserRecord> ReadAll() {
			throw new NotImplementedException();
		}

		public async Task<UserRecord?> ReadByLogin(string login) {
			await using var cn = await connections.OpenAsync();
			return await cn.TextCommand("SELECT * FROM auth.login WHERE login = @name")
				.WithInput("@name", login)
				.ExecuteReaderAsync()
				.ConsumeAsync(r => new UserRecord(
					r.Field<long>("id"),
					r.Field<string>("login")!) {
					PasswordHash = r.Field<string>("auth_text")!
				})
				.SingleOrDefaultAsync();
		}

		public Task Update(UserRecord item) {
			throw new NotImplementedException();
		}
	}
}