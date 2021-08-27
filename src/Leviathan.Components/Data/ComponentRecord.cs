using Leviathan.Data;

namespace Leviathan.Components.Data {
	public record ComponentRecord : DataItem<long> {
		public long AssemblyId { get; set; }
		public string TypeName { get; set; }
	}
}
