using Iot.Device.OneWire;
using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheLeviathan.OneWire {

	public interface IOneWireService : IHostedService {
		Task<IEnumerable<string>> BusIdsAsync();
		IEnumerable<OneWireDeviceListing> ListDevices();
		IReadOnlyDictionary<string, OneWireDevice> Devices { get; }
	}

	public class OneWireDeviceListing {
		public string BusId { get; init; }
		public string DeviceId { get; init; }
		public DeviceFamily Family { get; init; }
	}

	[HostedSingletonService(typeof(IOneWireService))]
	public class OneWireService : IOneWireService {

		Dictionary<string, OneWireBus> _buses;
		Dictionary<string, OneWireDevice> _devices;
		public IReadOnlyDictionary<string, OneWireDevice> Devices { get; }

		public OneWireService() {

			_buses = OneWireBus.EnumerateBusIds()
				.Select(id => new OneWireBus(id))
				.ToDictionary(i => i.BusId);

			_devices = _buses.Values.SelectMany(
				b => b.EnumerateDeviceIds(),
				(b, id) => new OneWireDevice(b.BusId, id)
			).ToDictionary(d => d.DeviceId);

			Devices = new ReadOnlyDictionary<string, OneWireDevice>(_devices);
		}

		public Task<IEnumerable<string>> BusIdsAsync() =>
			Task.FromResult(OneWireBus.EnumerateBusIds());

		public IEnumerable<OneWireDeviceListing> ListDevices() => Devices.Values.Select(d => new OneWireDeviceListing {
			BusId = d.BusId,
			DeviceId = d.DeviceId,
			Family = d.Family
		});

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}
}