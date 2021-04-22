using System;
using System.Collections.Generic;
using System.Linq;
using Leviathan.DataAccess;
using Leviathan.Model;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.DB.Npgsql {
	public class AuthDataRepo : NpgSqlRepo<AuthData>, IAuthDataRepository {

		public AuthDataRepo(NpgsqlConnection connection) : base(connection) { }

		public IEnumerable<AuthData> List() => CreateCommand(Queries.List)
			.ExecuteRead(r => r.Consume(r => new AuthData {
				Id = r.Field<int>("id"),
				AccountId = r.Field<int>("account_id"),
				AuthDataTypeId = r.Field<int>("auth_data_type_id"),
				AuthString = r.Field<string>("auth_string")
			}).ToArray());

		public override AuthData Create(AuthData item) => CreateCommand(Queries.Create)
			.WithInput("@account_id", item.AccountId)
			.WithInput("@auth_data_type_id", item.AuthDataTypeId)
			.WithInput("@auth_string", item.AuthString)
			.ExecuteReadSingle(r => {
				item.Id = r.Field<int>("id");
				return item;
			});

		public override AuthData Read(int id) => CreateCommand(Queries.Read)
			.ExecuteReadSingle(r => new AuthData {
				Id = r.Field<int>("id"),
				AccountId = r.Field<int>("account_id"),
				AuthDataTypeId = r.Field<int>("auth_data_type_id"),
				AuthString = r.Field<string>("auth_string")
			});

		public override AuthData Update(AuthData item) => CreateCommand(Queries.Update)
			.WithInput("@id", item.Id)
			.WithInput("@account_id", item.AccountId)
			.WithInput("@auth_data_type_id", item.AuthDataTypeId)
			.WithInput("@auth_string", item.AuthString)
			.ExecuteResult(
				c => c.ExecuteNonQuery(),
				r => item
			);

		public override void Delete(int itemId) => CreateCommand(Queries.Delete)
			.WithInput("@id", itemId)
			.ExecuteNonQuery();

		private static class Queries {
			public static readonly string List = LoadLocalResource("Queries.AuthData.List.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.AuthData.Read.sqlx");
			public static readonly string Create = LoadLocalResource("Queries.AuthData.Create.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.AuthData.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.AuthData.Delete.sqlx");
		}
	}
}