using System;


namespace Leviathan.Components {
	[AttributeUsage(AttributeTargets.Assembly)]
	public class LeviathanPluginAttribute : Attribute {

		public string Name { get; }

		public LeviathanPluginAttribute(string pluginName) {
			this.Name = pluginName;
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanConnectorAttribute : LeviathanComponentAttribute {

		public LeviathanConnectorAttribute(string name, string description) :
			base(name, description, ComponentCategory.Connector) {
		}
	}
}