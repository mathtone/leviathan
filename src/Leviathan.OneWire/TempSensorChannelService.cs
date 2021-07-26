using Iot.Device.OneWire;
using Leviathan.Channels.SDK;
using Leviathan.Services.SDK;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.OneWire {
	[SingletonService(typeof(ITempSensorService)), ChannelProvider]
	public class TempSensorChannelService : LeviathanService, ITempSensorService, IChannelProvider {

		IEnumerable<IChannel> _channels;
		readonly ILogger<TempSensorChannelService> _log;
		readonly ILogger<TempSensorChannel> _channelLog;
		public override Task Initialize { get; }

		public IEnumerable<IChannel> Channels => _channels ??= WhenInitialized(() => _channels).Result;

		public TempSensorChannelService(ILogger<TempSensorChannelService> log, ILogger<TempSensorChannel> channelLog) {
			(_log, _channelLog) = (log, channelLog);
			Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {
			await base.Initialize;
			_channels = GetOneWireThermometerDevices()
				.Select(d => new TempSensorChannel(_channelLog, d))
				.ToArray();

			_log.LogInformation($"{typeof(TempSensorChannelService)} Initialized");
		}

		static IEnumerable<OneWireThermometerDevice> GetOneWireThermometerDevices() {
			foreach (var busId in OneWireBus.EnumerateBusIds()) {

				var bus = new OneWireBus(busId);

				foreach (string devId in bus.EnumerateDeviceIds()) {
					if (OneWireThermometerDevice.IsCompatible(busId, devId)) {
						yield return new OneWireThermometerDevice(busId, devId);
					}
				}
			}
		}
	}

	//public class TempSensorChannel : IAsyncInputChannel<TempReading> {

	//	TempSensorChannelData _channelData;
	//	OneWireThermometerDevice _device;

	//	public TempSensorChannel(TempSensorChannelData data) {
	//		this._channelData = data;
	//		this._device = new OneWireThermometerDevice(_channelData.BusId, _channelData.DeviceId);
	//	}

	//	public async Task<TempReading> GetValue() {
	//		var temp = await _device.ReadTemperatureAsync();
	//		return new TempReading {
	//			Celsius = temp.DegreesCelsius,
	//			Farenheit = temp.DegreesFahrenheit,
	//			Kelvin = temp.Kelvins
	//		};
	//	}
	//}

	//public interface IChannelProvider { }
	//public interface IInputChannelProvider { }
	//public interface IOutputChannelProvider { }
	//public interface IInputOutputChannelProvider : IInputChannelProvider, IOutputChannelProvider { }

	//public interface ITempSensorChannelService : IInputChannelProvider { }

	//[SingletonService(typeof(ITempSensorChannelService))]
	//public class TempSensorChannelService : ChannelProviderService, ITempSensorChannelService {

	//	ILoggingService _log;
	//	Dictionary<string, TempSensorChannel> _channels;

	//	public override Task Initialize { get; }

	//	public TempSensorChannelService(ILoggingService log) {
	//		this._log = log;
	//		this.Initialize = InitializeAsync();
	//	}

	//	async Task InitializeAsync() {
	//		await base.Initialize;
	//		foreach (var (BusId, DevId) in OneWireDevice.EnumerateDeviceIds()) {
	//			var ch = new TempSensorChannel(new TempSensorChannelData(BusId, DevId));

	//			;
	//		}
	//	}
	//}

	//public abstract class ChannelProviderService : LeviathanService {
	//	public ChannelProviderService() {

	//	}
	//}

	//public interface IOneWireService {

	//}

	//public class OneWireService {

	//}
}