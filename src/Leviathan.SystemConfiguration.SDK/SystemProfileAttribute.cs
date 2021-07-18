using Leviathan.Components.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemConfiguration.SDK {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SystemProfileAttribute : LeviathanComponentAttribute {
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class ProfilePropertyAttribute : Attribute {
		public string Name { get; init; }
		public string Description { get; init; }
		public object DefaultValue{ get; init; }

		public ProfilePropertyAttribute(string name, string description, object defaultValue=default) {
			this.Name = name;
			this.Description = description;
			this.DefaultValue = defaultValue;
		}
	}
	public interface ISystemProfile {
		Task Apply();
	}

	public abstract class SystemProfileComponent : ISystemProfile {
		public abstract Task Apply();
	}
}