using Leviathan.Components.SDK;
using System;

namespace Leviathan.SystemConfiguration.SDK {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RequireProfileAttribute : Attribute {

		public Type Type { get; }

		public RequireProfileAttribute(Type profileType) {
			this.Type = profileType;
		}
	}
}