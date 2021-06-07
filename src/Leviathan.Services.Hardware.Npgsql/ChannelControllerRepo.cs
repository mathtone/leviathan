using Leviathan.DataAccess;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Hardware;
using Leviathan.Services.Core.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {

	public class ChannelControllerRepo : IChannelControllerData {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ChannelControllerRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public ChannelControllerInfo Create(ChannelControllerInfo item) => Provider.CreateConnection()
			.Used(c => {
				item.Id = c.CreateCommand(Queries.Create)
					.WithInput("@module_id", item.Name)
					.WithInput("@controller_type_id", item.ControllerTypeId)
					.WithInput("@channel_id", item.ChannelId)
					.WithInput("@name", item.Name)
					.ExecuteReadSingle(r => r.Field<int>(0));
				return item;
			});

		public void Delete(int itemId) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Delete)
				.WithInput("@id", itemId)
				.ExecuteNonQuery()
			);

		public IEnumerable<ChannelControllerInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public ChannelControllerInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public ChannelControllerInfo Update(ChannelControllerInfo item) {
			throw new NotImplementedException();
		}

		public static ChannelControllerInfo FromRecord(IDataRecord reader) => new() {
			Id = reader.Field<int>("id"),
			ChannelId = reader.Field<int>("channel_id"),
			ControllerTypeId = reader.Field<int>("controller_type_id"),
			Name = reader.Field<string>("name")
		};

		public IEnumerable<ChannelControllerCatalogItem> Catalog() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Catalog)
				.ExecuteReader()
				.Consume(r => new ChannelControllerCatalogItem {
					Id = r.Field<int>("id"),
					Name = r.Field<string>("controller_name"),
					TypeName = r.Field<string>("type_name"),
					ChannelName = r.Field<string>("channel_name"),
				})
				.ToArray()
			);

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.ChannelController.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.ChannelController.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.ChannelController.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.ChannelController.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.ChannelController.List.sqlx");
			public static readonly string Catalog = LoadLocalResource("Queries.ChannelController.Catalog.sqlx");
		}
	}
}