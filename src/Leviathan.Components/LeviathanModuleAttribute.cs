using System;


namespace Leviathan.Components {
	[AttributeUsage(AttributeTargets.Assembly)]
	public class LeviathanModuleAttribute : Attribute {

		public string Name { get; }

		public LeviathanModuleAttribute(string moduleName) {
			this.Name = moduleName;
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanConnectorAttribute : LeviathanComponentAttribute {

		public LeviathanConnectorAttribute(string name, string description) :
			base(name, description, ComponentCategory.Connector) {
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanChannelAttribute : LeviathanComponentAttribute {

		public LeviathanChannelAttribute(string name, string description) :
			base(name, description, ComponentCategory.HardwareChannel) {
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanApiAttribute : LeviathanComponentAttribute {

		public LeviathanApiAttribute(string name, string description) :
			base(name, description, ComponentCategory.ApiModule) {
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanServiceAttribute : LeviathanComponentAttribute {

		public LeviathanServiceAttribute(string name, string description) :
			base(name, description, ComponentCategory.Service) {
		}
	}
}