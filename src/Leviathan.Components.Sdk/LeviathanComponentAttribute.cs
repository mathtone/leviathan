using System;

namespace Leviathan.Components.Sdk {
	public abstract class LeviathanComponentAttribute : Attribute {
		public string ComponentTypeDescription { get; }
		public LeviathanComponentAttribute(string componentTypeDescription) {
			ComponentTypeDescription = componentTypeDescription;
		}
	}
}