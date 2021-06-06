using Leviathan.DataAccess;
using Leviathan.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {
	public class ChannelControllerRepo : IListRepository<ChannelControllerInfo, int> {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ChannelControllerRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public ChannelControllerInfo Create(ChannelControllerInfo item) {
			throw new NotImplementedException();
		}

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

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.ChannelController.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.ChannelController.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.ChannelController.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.ChannelController.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.ChannelController.List.sqlx");
		}
	}
}