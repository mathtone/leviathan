using Leviathan.DataAccess;
using Leviathan.Hardware;
using Leviathan.Services.Core.Hardware;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Leviathan.Utilities.ResourceLoader;
namespace Leviathan.Services.Hardware.Npgsql.Modules {

	public class ModuleTypeRepo : IHardwareModuleTypeData {

		protected IDbConnectionProvider<NpgsqlConnection> Provider { get; }

		public ModuleTypeRepo(IDbConnectionProvider<NpgsqlConnection> provider) {
			this.Provider = provider;
		}

		public HardwareModuleTypeInfo Create(HardwareModuleTypeInfo item) => Provider.CreateConnection()
			.Used(c => {
				item.ModuleTypeId = c.CreateCommand(Queries.Create)
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

		public IEnumerable<HardwareModuleTypeInfo> List() => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.List)
				.ExecuteReader()
				.Consume(FromRecord)
				.ToArray()
			);

		public HardwareModuleTypeInfo Read(int id) => Provider.CreateConnection()
			.Used(c => c.CreateCommand(Queries.Read)
				.WithInput("@id", id)
				.ExecuteReadSingle(FromRecord)
			);

		public HardwareModuleTypeInfo Update(HardwareModuleTypeInfo item) {
			throw new NotImplementedException();
		}

		public static HardwareModuleTypeInfo FromRecord(IDataRecord reader) => new() {
			ModuleTypeId = reader.Field<int>("id"),
			Name = reader.Field<string>("name"),
			TypeInfo = reader.Field<string>("type"),
		};

		private static class Queries {
			public static readonly string Create = LoadLocalResource("Queries.ModuleType.Create.sqlx");
			public static readonly string Read = LoadLocalResource("Queries.ModuleType.Read.sqlx");
			public static readonly string Update = LoadLocalResource("Queries.ModuleType.Update.sqlx");
			public static readonly string Delete = LoadLocalResource("Queries.ModuleType.Delete.sqlx");
			public static readonly string List = LoadLocalResource("Queries.ModuleType.List.sqlx");
		}
	}
}