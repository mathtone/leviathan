using System;

namespace Leviathan.Components {
	public class ComponentInfo {
		public int Id { get; init; }
		public Type Type { get; set; }
		public LeviathanComponentAttribute[] ComponentAttributes { get; set; }
	}
}