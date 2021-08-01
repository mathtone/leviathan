using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.Channels {

	public interface IChannelsService : IHostedService { }

	[HostedSingletonService(typeof(IChannelsService))]
	public class ChannelsService : IChannelsService {

		ILogger _log;

		public ChannelsService(ILogger<ChannelsService> log) {
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