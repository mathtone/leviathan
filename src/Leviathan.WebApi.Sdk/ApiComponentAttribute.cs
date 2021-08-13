using Leviathan.Components.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.WebApi {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ApiComponentAttribute : LeviathanComponentAttribute {

		public string ModuleName { get; }

		public ApiComponentAttribute(string moduleName) {
			this.ModuleName = moduleName;
		}
	}
}
