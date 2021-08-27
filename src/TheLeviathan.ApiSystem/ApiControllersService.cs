using Leviathan.Components;
using Leviathan.Services;
using Leviathan.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLeviathan.ComponentSystem;

namespace TheLeviathan.ApiSystem {
	public interface IApiControllersService {
		IEnumerable<ComponentInfo> ControllerTypes();
	}

	[SingletonService(typeof(IApiControllersService))]
	public class ApiControllersService : IApiControllersService {

		IComponentsService _components;

		public ApiControllersService(IComponentsService components) {
			_components = components;
		}

		public IEnumerable<ComponentInfo> ControllerTypes() =>
			_components.GetLeviathanComponents<ApiComponentAttribute>();
	}
}