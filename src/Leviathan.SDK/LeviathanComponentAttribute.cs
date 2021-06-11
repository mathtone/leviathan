using System;

namespace Leviathan.SDK {

	

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanComponentAttribute : Attribute {

		public string Name { get; }
		public string Description { get; }
		public string Category { get; }

		public LeviathanComponentAttribute(string name, string category, string description) {
			this.Name = name;
			this.Category = category;
			this.Description = description;
		}
	}
}