using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.ApiSystem {

	public interface IComponentsService : IHostedService {
		IEnumerable<Assembly> ListAssemblies();
	}

	public interface IApiControllersService : IHostedService {
		IEnumerable<Type> ControllerTypes();
	}

	[HostedSingletonService(typeof(IComponentsService))]
	public class ComponentsService : IComponentsService {
		public ComponentsService() {
			;
		}

		public IEnumerable<Assembly> ListAssemblies() {
			var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			foreach (var f in Directory.GetFiles(location, "*.dll")) {
				yield return Assembly.Load(AssemblyName.GetAssemblyName(f));
			}
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			//throw new System.NotImplementedException();
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}

	[HostedSingletonService(typeof(IApiControllersService))]
	public class ApiControllersService : IApiControllersService {

		IComponentsService _components;

		public ApiControllersService(IComponentsService components) {
			_components = components;
		}

		public IEnumerable<Type> ControllerTypes() {
			foreach (var listing in _components.ListAssemblies()) {
				var assembly = Assembly.Load(AssemblyName.GetAssemblyName(listing.Location));
				foreach (var type in assembly.GetExportedTypes()) {
					var attr = type.GetCustomAttribute<ApiComponentAttribute>();
					if (attr != null) {
						yield return type;
					}
				}
			}
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			//throw new System.NotImplementedException();
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}