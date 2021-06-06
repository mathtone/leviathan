using Leviathan.DataAccess;
using Leviathan.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {
	public class ChannelTypeRepo : IListRepository<ChannelTypeInfo, int> {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ChannelTypeRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public ChannelTypeInfo Create(ChannelTypeInfo item) {
			throw new NotImplementedException();
		}

		public void Delete(int itemId) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Delete)
				.WithInput("@id", itemId)
				.ExecuteNonQuery()
			);

		public IEnumerable<ChannelTypeInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public ChannelTypeInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public ChannelTypeInfo Update(ChannelTypeInfo item) {
			throw new NotImplementedException();
		}

		public static ChannelTypeInfo FromRecord(IDataRecord reader) => new() {
			Id = reader.Field<int>("id"),
			Name = reader.Field<string>("name"),
			TypeInfo = reader.Field<string>("type"),
		};

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.ChannelType.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.ChannelType.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.ChannelType.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.ChannelType.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.ChannelType.List.sqlx");
		}
	}
}