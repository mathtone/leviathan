using Leviathan.SystemHost;
using Microsoft.Extensions.Hosting;

namespace Leviathan.SystemHost.Support {
	public class SystemHostService : ISystemHostService, IHostedService {

		private readonly SystemHostConfig _config;

		public bool IsInitialized =>_config.IsInitialized;

		public SystemHostService(SystemHostConfig config) =>
			_config = config;

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}