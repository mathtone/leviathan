using Leviathan.Services.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {

	public interface IComponentFactory {

	}

	[SingletonService(typeof(IComponentFactory))]
	public class ComponentFactoryService : LeviathanService, IComponentFactory {

		IServiceProvider _services;
		ILogger<ComponentFactoryService> _log;

		public override Task Initialize { get; }

		public ComponentFactoryService(ILogger<ComponentFactoryService> log, IServiceProvider services) =>
			(_services, _log, Initialize) = (services, log, InitializeAsync());

		async Task InitializeAsync() {
			await base.Initialize;
			_log.Log(LogLevel.Information, $"{typeof(ComponentInfo)}");
		}

		public static object Create(Type contractType, Type implementationType, params object[] args) {
			return default;
		}
	}
}