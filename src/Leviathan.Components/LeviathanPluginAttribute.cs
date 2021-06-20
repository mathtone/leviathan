using System;

namespace Leviathan.Components {
	[AttributeUsage(AttributeTargets.Assembly)]
	public class LeviathanPluginAttribute : Attribute { }
	
	public class SystemProfileAttribute : LeviathanComponentAttribute {
		public SystemProfileAttribute(string name, string description) : base(name, description, ComponentCategory.SystemProfile) {
		}
	}
}
