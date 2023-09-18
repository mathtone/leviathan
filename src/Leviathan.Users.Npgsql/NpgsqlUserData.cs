
using Leviathan.Data;
using System.Data.Common;
using Mathtone.Sdk.Data;
using Npgsql;
using Mathtone.Sdk.Data.Npgsql;

namespace Leviathan.Users.Npgsql {
   public class NpgsqlUserData(IConnectionProvider<NpgsqlConnection> connections) : IUserData {

      public async ValueTask<int> Create(UserRecord item) {
         await using var cn = await connections.OpenAsync();
         var rec = await cn.TextCommand("INSERT INTO system_user (name,password_hash) VALUES(@name,@pwd_hash) RETURNING id")
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
         var rec = await cn.TextCommand("SELECT * FROM system_user WHERE name = @name")
            .WithInput("name", login)
            .ExecuteReaderAsync()
            .ConsumeAsync(r => new UserRecord(r.Field<long>("id"), r.Field<string>("name")!) {
               PasswordHash = r.Field<string>("password_hash")!
            })
            .SingleOrDefaultAsync();
         return rec;
      }

      public Task Update(UserRecord item) {
         throw new NotImplementedException();
      }
   }
}