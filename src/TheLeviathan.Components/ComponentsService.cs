using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.Components {

	public interface IComponentsService : IHostedService {

	}

	[HostedSingletonService(typeof(IComponentsService))]
	public class ComponentsService : HostedServiceBase, IComponentsService {
		public ComponentsService(ILogger<ComponentsService> log) : base(log) { }
	}
}