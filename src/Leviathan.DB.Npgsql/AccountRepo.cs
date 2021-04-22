using System.Collections.Generic;
using System.Linq;
using Leviathan.DataAccess;
using Leviathan.Model;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.DB.Npgsql {
	public class AccountRepo : NpgSqlRepo<Account>, IAccountRepository {

		public AccountRepo(NpgsqlConnection connection) : base(connection) { }

		public IEnumerable<Account> List() => CreateCommand(Queries.List)
			.ExecuteRead(r => r.Consume(r => new Account {
				Id = r.Field<int>("id"),
				Login = r.Field<string>("login"),
				Name = r.Field<string>("Name"),
			}).ToArray());

		public override Account Create(Account item) => CreateCommand(Queries.Create)
			.WithInput("@id", item.Id)
			.WithInput("@name", item.Name)
			.WithInput("@login", item.Login)
			.ExecuteReadSingle(r => {
				item.Id = r.Field<int>("id");
				return item;
			});

		public override Account Read(int id) => CreateCommand(Queries.Read)
			.ExecuteReadSingle(r => new Account {
				Id = r.Field<int>("id"),
				Name = r.Field<string>("name"),
				Login = r.Field<string>("login")
			});

		public override Account Update(Account item) => CreateCommand(Queries.Update)
			.WithInput("@id", item.Id)
			.WithInput("@name", item.Name)
			.WithInput("@login", item.Login)
			.ExecuteResult(
				c => c.ExecuteNonQuery(),
				r => item
			);

		public override void Delete(int itemId) => CreateCommand(Queries.Delete)
			.WithInput("@id", itemId)
			.ExecuteNonQuery();

		private static class Queries {
			public static readonly string List = LoadLocalResource("Queries.Account.List.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.Account.Read.sqlx");
			public static readonly string Create = LoadLocalResource("Queries.Account.Create.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.Account.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.Account.Delete.sqlx");
		}
	}
}