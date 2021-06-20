namespace Leviathan.Alpha.Data.Npgsql {
	public record HardwareModuleRecord : StandardEntity<long> {
		public long ComponentTypeId { get; init; }
	}

	public class HardwareModuleRepo : AlphaDbDataRepo<HardwareModuleRecord> {

	}
}