using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface IHardwareConnectorRepo : IListRepository<long, HardwareConnectorRecord> {
	}
	public record HardwareConnectorRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
		public long ModuleId { get; init; }
		public object ConnectorData { get; init; }
	}

	public class HardwareConnectorRepo : AlphaDbListRepo<HardwareConnectorRecord>, IHardwareConnectorRepo {
		public HardwareConnectorRepo(NpgsqlConnection connection) : base(connection) {
		}

		public override long Create(HardwareConnectorRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@module_id", item.ModuleId)
			.WithInput("@connector_data", item.ConnectorData, NpgsqlDbType.Json)
			.ExecuteReadSingle(r=>r.GetInt64(0));

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<HardwareConnectorRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override HardwareConnectorRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(HardwareConnectorRecord item) => Connect()
			.CreateCommand(SQL.UPDATE)
			.WithInput("@id", item.Name)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@module_id", item.ModuleId)
			.WithInput("@connector_data", item.ConnectorData, NpgsqlDbType.Json)
			.ExecuteNonQuery();

		private static HardwareConnectorRecord FromData(IDataRecord record) => new() {
			Id = record.Field<long>("id"),
			Name = record.Field<string>("name"),
			Description = record.Field<string>("description"),
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.hardware_connector (
					component_type_id,	
					module_id,
					name,
					description,
					connector_data
				)
				VALUES(
					@component_type_id,
					@module_id,
					@name,
					@description,
					@connector_data
				)
				RETURNING id;",

			UPDATE = @"
				UPDATE sys.hardware_connector SET
					name=@name,
					description=@description,
					component_type_id=@component_type_id,
					module_id=@module_id,
					connetor_data=@connector_data
				WHERE id=@id",

			LIST = @"SELECT * FROM sys.hardware_connector",
			READ = @"SELECT * FROM sys.hardware_connector WHERE id=@id",
			DELETE = @"DELETE sys.hardware_connector WHERE id=@id",
		};
	}
}