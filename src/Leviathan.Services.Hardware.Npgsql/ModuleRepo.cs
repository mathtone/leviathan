﻿using Leviathan.DataAccess;
using Leviathan.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {
	public class ModuleRepo : IListRepository<HardwareModuleInfo, int> {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ModuleRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public HardwareModuleInfo Create(HardwareModuleInfo item) {
			throw new NotImplementedException();
		}

		public void Delete(int itemId) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Delete)
				.WithInput("@id", itemId)
				.ExecuteNonQuery()
			);

		public IEnumerable<HardwareModuleInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public HardwareModuleInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public HardwareModuleInfo Update(HardwareModuleInfo item) {
			throw new NotImplementedException();
		}

		public static HardwareModuleInfo FromRecord(IDataRecord reader) => new() {
			Id = reader.Field<int>("id"),
			Name = reader.Field<string>("name"),
			TypeId = reader.Field<int>("module_type_id"),
		};

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.Module.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.Module.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.Module.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.Module.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.Module.List.sqlx");
		}
	}
}