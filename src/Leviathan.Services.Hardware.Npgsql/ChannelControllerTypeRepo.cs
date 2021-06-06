using Leviathan.DataAccess;
using Leviathan.Hardware;
using Leviathan.Services.Core.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {
	public class ChannelControllerTypeRepo : IChannelControllerTypeData {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ChannelControllerTypeRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public ChannelControllerTypeInfo Create(ChannelControllerTypeInfo item) => Provider.CreateConnection()
			.Used(c => {
				item.Id = c.CreateCommand(Queries.Create)
					.WithInput("@name", item.Name)
					.WithInput("@type", item.TypeInfo)
					.ExecuteReadSingle(r => r.Field<int>(0));
				return item;
			});

		public void Delete(int itemId) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Delete)
				.WithInput("@id", itemId)
				.ExecuteNonQuery()
			);

		public IEnumerable<ChannelControllerTypeInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public ChannelControllerTypeInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public ChannelControllerTypeInfo Update(ChannelControllerTypeInfo item) {
			throw new NotImplementedException();
		}

		public static ChannelControllerTypeInfo FromRecord(IDataRecord reader) => new() {
			Id = reader.Field<int>("id"),
			Name = reader.Field<string>("name"),
			TypeInfo = reader.Field<string>("type"),
		};

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.ChannelControllerType.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.ChannelControllerType.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.ChannelControllerType.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.ChannelControllerType.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.ChannelControllerType.List.sqlx");
		}
	}
}