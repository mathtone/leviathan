using Leviathan.DbDataAccess.Npgsql;
using Npgsql;
using System;

namespace Leviathan.Alpha.Data.Npgsql {

	public record HardwareConnectorRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
		public object ConnectorData { get; init; }
	}

	public class HardwareConnectorRepo : AlphaDbDataRepo<HardwareConnectorRecord> {
		public HardwareConnectorRepo(IRepoCommands commands, NpgsqlConnection connection) : base(commands, connection) {
		}

		public override long Create(HardwareConnectorRecord item) {
			throw new NotImplementedException();
		}

		public override void Delete(long id) {
			throw new NotImplementedException();
		}

		public override HardwareConnectorRecord Read(long id) {
			throw new NotImplementedException();
		}

		public override void Update(HardwareConnectorRecord item) {
			throw new NotImplementedException();
		}
	}
}