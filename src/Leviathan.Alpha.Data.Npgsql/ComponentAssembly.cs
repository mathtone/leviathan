namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentAssemblyRecord : StandardEntity<long> {
		public string AssemblyName { get; init; }
		public string AssemblyPath { get; init; }
	}

	public class ComponentAssemblyRepo : AlphaDbDataRepo<ComponentAssemblyRecord> {
	
	}
}