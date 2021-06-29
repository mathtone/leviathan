using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface IHardwareChannelRepo : IListRepository<long, HardwareChannelRecord> {
	}

	public record HardwareChannelRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
		public long ConnectorId { get; init; }
		public object ChannelData { get; init; }
	}

	public class HardwareChannelRepo : AlphaDbListRepo<HardwareChannelRecord>, IHardwareChannelRepo {

		public HardwareChannelRepo(NpgsqlConnection connection) :
			base(connection) { }

		public override long Create(HardwareChannelRecord item) =>Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@connector_id", item.ConnectorId)
			.WithInput("@channel_data", item.ChannelData, NpgsqlDbType.Json)
			.ExecuteReadSingle(r => r.GetInt64(0));

		public override void Delete(long id) => Connect()
			.CreateCommand(SQL.DELETE)
			.ExecuteNonQuery();

		public override IEnumerable<HardwareChannelRecord> List() => Connect()
			.CreateCommand(SQL.LIST)
			.ExecuteReader()
			.ToArray(FromData);

		public override HardwareChannelRecord Read(long id) => Connect()
			.CreateCommand(SQL.READ)
			.WithInput("@id", id)
			.ExecuteReadSingle(FromData);

		public override void Update(HardwareChannelRecord item) => Connect()
			.CreateCommand(SQL.CREATE)
			.WithInput("@id", item.Id)
			.WithInput("@name", item.Name)
			.WithInput("@description", item.Description)
			.WithInput("@component_type_id", item.ComponentTypeId)
			.WithInput("@connector_id", item.ConnectorId)
			.WithInput("@channel_data", item.ChannelData, NpgsqlDbType.Json)
			.ExecuteNonQuery();	

		private static HardwareChannelRecord FromData(IDataRecord record) => new() {
			Id = record.Get<long>("id"),
			Name = record.Get<string>("name"),
			Description = record.Get<string>("description"),
			ComponentTypeId = record.Get<long>("component_type_id"),
			ConnectorId = record.Get<long>("connector_id"),
			ChannelData = record.Get<object>("channel_data")
		};

		private static readonly IListRepoCommands SQL = new ListRepoCommands {
			CREATE = @"
				INSERT INTO sys.hardware_channel (
					connector_id,					
					component_type_id,	
					name,
					description,
					channel_data
				)
				VALUES(
					@connector_id,
					@component_type_id,
					@name,
					@description,
					@channel_data
				)
				RETURNING id;",

			UPDATE = @"
				UPDATE sys.hardware_channel SET
					name=@name,
					description=@description,
					component_type_id=@component_type_id,
					connector_id=@module_id,
					connetor_data=@connector_data
				WHERE id=@id",

			LIST = @"SELECT * FROM sys.hardware_channel",
			READ = @"SELECT * FROM sys.hardware_channel WHERE id=@id",
			DELETE = @"DELETE sys.hardware_channel WHERE id=@id",
		};
	}
}