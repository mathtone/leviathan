using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.CoreSystem {
	public interface ICoreService : IHostedService {

	}

	[HostedSingletonService(typeof(ICoreService))]
	public class CoreService : ICoreService {

		public CoreService() { }

		public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}
