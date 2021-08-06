using Leviathan.Components.Sdk;
using System;

namespace Leviathan.WebApi.Sdk {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ApiComponentAttribute : LeviathanComponentAttribute {
		
		public string ModuleName { get; }

		public ApiComponentAttribute(string moduleName) {
			this.ModuleName = moduleName;
		}
	}
}