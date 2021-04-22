using System.Collections.Generic;
using System.Linq;
using Leviathan.DataAccess;
using Leviathan.Model;
using Npgsql;
using static Leviathan.Utilities.ResourceLoader;

namespace Leviathan.DB.Npgsql {
	public class ConnectorRepo : NpgSqlRepo<Connector>, IConnectorRepository {

		public ConnectorRepo(NpgsqlConnection connection) : base(connection) { }

		public IEnumerable<Connector> List() => CreateCommand(Queries.List)
			.ExecuteRead(r => r.Consume(r => new Connector {
				Id = r.Field<int>("id"),
				//ConnectorTypeId = r.Field<int>("connector_type_id"),
				ControllerId = r.Field<int>("controller_id"),
				Descripion = r.Field<string>("description"),
			}).ToArray());

		public override Connector Create(Connector item) => CreateCommand(Queries.Create)
			.WithInput("@id", item.Id)
			.WithInput("@controllerId", item.ControllerId)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Descripion)
			.ExecuteReadSingle(r => {
				item.Id = r.Field<int>("id");
				return item;
			});

		public override Connector Read(int id) => CreateCommand(Queries.Read)
			.ExecuteReadSingle(r => new Connector {
				Id = r.Field<int>("id"),
				//ConnectorTypeId = r.Field<int>("connector_type_id"),
				ControllerId = r.Field<int>("controller_id"),
				Descripion = r.Field<string>("description"),
			});

		public override Connector Update(Connector item) => CreateCommand(Queries.Update)
			.WithInput("@id", item.Id)
			.WithInput("@controllerId", item.ControllerId)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Descripion)
			.ExecuteResult(
				c => c.ExecuteNonQuery(),
				r => item
			);

		public override void Delete(int itemId) => CreateCommand(Queries.Delete)
			.WithInput("@id", itemId)
			.ExecuteNonQuery();

		private static class Queries {
			public static readonly string List = LoadLocalResource("Queries.Connector.List.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.Connector.Read.sqlx");
			public static readonly string Create = LoadLocalResource("Queries.Connector.Create.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.Connector.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.Connector.Delete.sqlx");
		}
	}
}