using Leviathan.Services;
using Leviathan.WebApi;
using System;
using System.Collections.Generic;
using TheLeviathan.ComponentSystem;

namespace TheLeviathan.ApiSystem {
	
	public interface IApiControllersService {
		IEnumerable<Type> ControllerTypes();
	}

	public static partial class ServiceConfiguration {
		
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
}
