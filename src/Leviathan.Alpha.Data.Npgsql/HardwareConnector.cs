using System;

namespace Leviathan.Alpha.Data.Npgsql {

	public record HardwareConnectorRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
		public object ConnectorData { get; init; }
	}

	public class HardwareConnectorRepo : AlphaDbDataRepo<HardwareConnectorRecord> {

	}
}