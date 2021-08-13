using Iot.Device.OneWire;
using Leviathan.Channels.Sdk;
using Leviathan.Services.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Drivers.OneWire {

	public interface IOneWireService {
		IReadOnlyDictionary<string, OneWireBus> Buses { get; }
		IEnumerable<string> GetDeviceIds(DeviceFamily family = DeviceFamily.Any);
		IEnumerable<string> GetBusIds();
	}

	[SingletonService(typeof(IOneWireService))]
	public class OneWireService : IOneWireService {

		public IReadOnlyDictionary<string, OneWireBus> Buses { get; }

		public OneWireService() {
			Buses = OneWireBus
				.EnumerateBusIds()
				.ToDictionary(id => id, id => new OneWireBus(id));
		}

		public IEnumerable<string> GetBusIds() =>
			Buses.Keys;

		public IEnumerable<string> GetDeviceIds(DeviceFamily family = DeviceFamily.Any) =>
			Buses.Values.SelectMany(b => b.EnumerateDeviceIds(family));
	}

	//[SingletonService(typeof(IChannelProvider))]
	//public class TemperatureChannelProvider : IChannelProvider {

	//	IOneWireService _oneWire;
	//	IEnumerable<IChannel> _channels;
	//	IChannelDataService _channelData;

	//	public TemperatureChannelProvider(IOneWireService oneWire, IChannelDataService channelData) {
	//		(_oneWire, _channelData) = (oneWire, channelData);
	//	}

	//	public IEnumerable<IChannel> GetChannels() => _channels ??=
	//		_oneWire.Buses.Values.SelectMany(b =>
	//			b.EnumerateDeviceIds(DeviceFamily.Thermometer)
	//				.Select(d => new TemperatureInputChannel(new(b.BusId, d)))
	//		);
	//}

	public class OneWireChannelData {
		public int ChannelId { get; set; }
		public string Name { get; set; }
	}

	public class TemperatureInputChannel : AsyncInputChannelBase<TempReading> {

		IOneWireService _oneWire;
		OneWireChannelData _data;

		public TemperatureInputChannel(IOneWireService oneWire, OneWireChannelData data) {
			(_oneWire, _data) = (oneWire, data);
		}

		public override async Task<TempReading> GetAsync() {
			return default;

			//var temp = await _device.ReadTemperatureAsync();
			//return new TempReading {
			//	Celsius = temp.DegreesCelsius,
			//	Farenheit = temp.DegreesFahrenheit,
			//	Kelvin = temp.Kelvins
			//};
		}
	}

	public struct TempReading {
		public double Celsius { get; init; }
		public double Farenheit { get; init; }
		public double Kelvin { get; init; }
	}
}