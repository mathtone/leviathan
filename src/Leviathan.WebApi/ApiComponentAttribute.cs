using Leviathan.Components;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Leviathan.WebApi {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ApiComponentAttribute : LeviathanComponentAttribute {
		public string Name { get; }
		public string ModuleName { get; }

		public ApiComponentAttribute(string name, string moduleName) : base("Api Component") {
			(Name, ModuleName) = (name, moduleName);
		}
	}

	public abstract class ServiceController<T> : LeviathanApiController
	{
		protected T Service { get; }
		public ServiceController(T service) => Service = service;
	}

	public abstract class LeviathanApiController : ControllerBase
	{

	}
}