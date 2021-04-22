using System.Collections.Generic;
using System.Linq;
using Leviathan.DataAccess;
using Leviathan.Model;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.DB.Npgsql {
	public class ControllerRepo : NpgSqlRepo<Controller,int>, IControllerRepository {

		public ControllerRepo(NpgsqlConnection connection) : base(connection) { }

		public IEnumerable<Controller> List() => CreateCommand(Queries.List)
			.ExecuteRead(r => r.Consume(r => new Controller {
				Id = r.Field<int>("id"),
				ControllerTypeId = r.Field<int>("controller_type_id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("login")
			}).ToArray());

		public override Controller Create(Controller item) => CreateCommand(Queries.Create)
			.WithInput("@controller_type_id", item.ControllerTypeId)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteReadSingle(r => {
				item.Id = r.Field<int>("id");
				return item;
			});

		public override Controller Read(int id) => CreateCommand(Queries.Read)
			.ExecuteReadSingle(r => new Controller {
				Id = r.Field<int>("id"),
				ControllerTypeId = r.Field<int>("controller_type_id"),
				Name = r.Field<string>("name"),
				Description = r.Field<string>("login")
			});

		public override Controller Update(Controller item) => CreateCommand(Queries.Update)
			.WithInput("@id", item.Id)
			.WithInput("@controller_type_id", item.ControllerTypeId)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.ExecuteResult(
				c => c.ExecuteNonQuery(),
				r => item
			);

		public override void Delete(int itemId) => CreateCommand(Queries.Delete)
			.WithInput("@id", itemId)
			.ExecuteNonQuery();

		private static class Queries {
			public static readonly string List = LoadLocalResource("Queries.Controller.List.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.Controller.Read.sqlx");
			public static readonly string Create = LoadLocalResource("Queries.Controller.Create.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.Controller.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.Controller.Delete.sqlx");
		}
	}
}