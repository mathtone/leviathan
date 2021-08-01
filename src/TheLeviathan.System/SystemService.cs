using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.System {
	public interface ISystemService : IHostedService {
	}

	[HostedSingletonService(typeof(ISystemService))]
	public class SystemService : ISystemService {

		ILogger _log;

		public SystemService(ILogger<SystemService> log) {
			_log = log;
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}