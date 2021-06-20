namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentTypeRecord : StandardEntity<long> {
		public long CategoryId { get; init; }
		public long AssemblyId { get; init; }
		public string TypeName { get; init; }
	}
	public class ComponentTypeRepo : AlphaDbDataRepo<ComponentTypeRecord> {

	}

}