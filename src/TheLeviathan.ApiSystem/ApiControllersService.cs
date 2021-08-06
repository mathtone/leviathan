using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TheLeviathan.ComponentsSystem;

namespace TheLeviathan.ApiSystem {

	public interface IApiControllersService {
		IEnumerable<Type> ControllerTypes();
	}

	[SingletonService(typeof(IApiControllersService))]
	public class ApiControllersService : IApiControllersService {

		IComponentsService _components;

		public ApiControllersService(IComponentsService components) {
			_components = components;
		}

		public IEnumerable<Type> ControllerTypes() =>
			_components.GetComponents<ApiComponentAttribute>();
	}
}