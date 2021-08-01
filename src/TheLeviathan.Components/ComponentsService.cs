using Leviathan.Services.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.Components {

	public interface IComponentsService : IHostedService {

	}

	[HostedSingletonService(typeof(IComponentsService))]
	public class ComponentsService : IComponentsService {

		ILogger _log;
		IServiceProvider _services;

		public ComponentsService(ILogger<ComponentsService> log, IServiceProvider services) =>
			(_log, _services) = (log, services);

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}