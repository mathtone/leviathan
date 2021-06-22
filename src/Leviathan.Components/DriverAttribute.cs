using System;


namespace Leviathan.Components {
	[AttributeUsage(AttributeTargets.Class)]
	public class DriverAttribute : LeviathanComponentAttribute {
		public object DriverData { get; }

		public DriverAttribute(string name, string description, object driverData = null) : base(name, description, ComponentCategory.Driver) {
			this.DriverData = driverData;
		}
	}
}