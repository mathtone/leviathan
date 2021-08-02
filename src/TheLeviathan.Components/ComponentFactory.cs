using Leviathan.Services.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.Extensions.DependencyInjection.ActivatorUtilities;

namespace TheLeviathan.Components {

	public interface IComponentFactory : IHostedService {
		T ActivateInstance<T>(params object[] parameters);
		object ActivateInstance(Type type, params object[] parameters);
	}

	[HostedSingletonService(typeof(IComponentFactory))]
	public class ComponentFactory : IComponentFactory {

		ILogger _log;
		IServiceProvider _services;

		public ComponentFactory(ILogger<ComponentFactory> log, IServiceProvider services) =>
			(_log, _services) = (log, services);

		public T ActivateInstance<T>(params object[] parameters) =>
			CreateInstance<T>(_services, parameters);

		public object ActivateInstance(Type type, params object[] parameters) => 
			CreateInstance(_services, type, parameters);

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}