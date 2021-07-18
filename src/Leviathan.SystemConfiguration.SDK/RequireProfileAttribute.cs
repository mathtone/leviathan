using Leviathan.Components.SDK;
using System;

namespace Leviathan.SystemConfiguration.SDK {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class RequireProfileAttribute : Attribute {

		public Type Type { get; }

		public RequireProfileAttribute(Type profileType) {
			this.Type = profileType;
		}
	}
}