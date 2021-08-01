using Iot.Device.OneWire;
using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.OneWire {

	public interface IOneWireService : IHostedService {
		Task<IEnumerable<string>> BusIdsAsync();
	}

	[HostedSingletonService(typeof(IOneWireService))]
	public class OneWireService : IOneWireService {

		public Task<IEnumerable<string>> BusIdsAsync() =>
			Task.FromResult(OneWireBus.EnumerateBusIds());

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}