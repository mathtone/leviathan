using System;


namespace Leviathan.Components {
	[AttributeUsage(AttributeTargets.Class)]
	public class SystemProfileAttribute : LeviathanComponentAttribute {
		public SystemProfileAttribute(string name, string description) : base(name, description, ComponentCategory.SystemProfile) {
		}
	}
}