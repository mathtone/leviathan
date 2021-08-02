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
	public class SystemService : HostedServiceBase, ISystemService {

		public SystemService(ILogger<SystemService> log):base(log) {		}

	}
}