using System;

namespace Leviathan.Components {

	public enum ComponentCategory {
		SystemProfile,
	}
	[AttributeUsage(AttributeTargets.Class)]
	public abstract class LeviathanComponentAttribute : Attribute {

		public string Name { get; init; }
		public string Description { get; init; }
		public ComponentCategory Category { get; init; }

		public LeviathanComponentAttribute(string name, string description, ComponentCategory category) {
			this.Name = name;
			this.Description = description;
			this.Category = category;
		}
	}
}