using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.Services.Sdk {

	public abstract class HostedServiceBase : ServiceBase,IHostedService {

		protected HostedServiceBase(ILogger<HostedServiceBase> log) :
			base(log) {}

		public async virtual Task StartAsync(CancellationToken cancellationToken) =>
			await Task.CompletedTask;

		public async virtual Task StopAsync(CancellationToken cancellationToken) =>
			await Task.CompletedTask;
	}
}