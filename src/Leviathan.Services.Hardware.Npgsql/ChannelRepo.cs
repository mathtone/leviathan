using Leviathan.DataAccess;
using Leviathan.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {
	public class ChannelRepo : IListRepository<ChannelInfo, int> {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ChannelRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public ChannelInfo Create(ChannelInfo item) {
			throw new NotImplementedException();
		}

		public void Delete(int itemId) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Delete)
				.WithInput("@id", itemId)
				.ExecuteNonQuery()
			);

		public IEnumerable<ChannelInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public ChannelInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public ChannelInfo Update(ChannelInfo item) {
			throw new NotImplementedException();
		}

		public static ChannelInfo FromRecord(IDataRecord reader) => new() {
			Id = reader.Field<int>("id"),
			ModuleId = reader.Field<int>("module_id"),
			ChannelTypeId = reader.Field<int>("channel_type_id"),
			Name = reader.Field<string>("name"),
			ChannelData = reader.Field<string>("channel_data"),

		};

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.Channel.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.Channel.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.Channel.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.Channel.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.Channel.List.sqlx");
		}
	}


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
			//ModuleId = reader.Field<int>("module_id"),
			//ChannelTypeId = reader.Field<int>("channel_type_id"),
			Name = reader.Field<string>("name"),
			//ChannelData = reader.Field<string>("channel_data"),

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