using System;

namespace TheLeviathan.ComponentSystem.Data {

	public record AssemblyRecord {
		public int Id { get; init; }
		public string Path { get; init; }
		public string Description { get; set; }
	}

	public record TypeRecord {
		public int Id { get; init; }
		public int AssemblyId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public record ComponentRecord {
		public int Id { get; init; }
		public int TypeId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public record ComponentCategory {
		public int Id { get; init; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}