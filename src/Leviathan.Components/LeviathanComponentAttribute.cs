using System;

namespace Leviathan.Components {

	public enum ComponentCategory {
		SystemProfile = 1,
		Driver = 2,
		Connector = 3,
		HardwareChannel = 4,
		Service = 4,
		ApiModule,
		Custom
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