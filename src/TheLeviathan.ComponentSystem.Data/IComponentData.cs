using Leviathan.Common;
using Leviathan.DataAccess;

namespace TheLeviathan.ComponentSystem.Data {
	public interface IComponentData {
		IAsyncRepository<int, AssemblyRecord> Assembly { get; }
	}

	//public record TypeRecord {
	//	public int Id { get; init; }
	//	public int AssemblyId { get; set; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}

	//public record ComponentRecord {
	//	public int Id { get; init; }
	//	public int TypeId { get; set; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}

	//public record ComponentCategory {
	//	public int Id { get; init; }
	//	public string Name { get; set; }
	//	public string Description { get; set; }
	//}
}
