using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface IHardwareModuleRepo : IListRepository<long, HardwareModuleRecord> {
	}
	public record HardwareModuleRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
		public object ModuleData { get; init; }
	}

	public class HardwareModuleRepo : AlphaDbListRepo<HardwareModuleRecord>, IHardwareModuleRepo {

		public HardwareModuleRepo(NpgsqlConnection connection) : base(connection) { }

		public override long Create(HardwareModuleRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@module_data", item.ModuleData??new object(), NpgsqlDbType.Json)
			.ExecuteReadSingle(r => r.Get<long>("id"));

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<HardwareModuleRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override HardwareModuleRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(HardwareModuleRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Name)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@module_data", item.ModuleData, NpgsqlDbType.Json)
			.ExecuteNonQuery();

		private static HardwareModuleRecord FromData(IDataRecord record) => new() {
			Id = record.Get<long>("id"),
			Name = record.Get<string>("name"),
			Description = record.Get<string>("description"),
			ComponentTypeId = record.Get<long>("component_type_id"),
			ModuleData = record.Get<object>("module_data"),
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.hardware_module (
					name,
					description,
					component_type_id,
					module_data
				)
				VALUES(
					@name,
					@description,
					@component_type_id,
					@module_data
				)
				RETURNING id",

			UPDATE = @"
				UPDATE sys.hardware_module SET
					name=@name,
					description=@description,
					component_type_id=@component_type_id,
					module_data=@module_data,
				WHERE id=@id",

			LIST = "SELECT * FROM sys.hardware_module",
			READ = "SELECT * FROM sys.hardware_module WHERE id=@id",
			DELETE = "DELETE sys.hardware_module WHERE id=@id",
		};
	}
}