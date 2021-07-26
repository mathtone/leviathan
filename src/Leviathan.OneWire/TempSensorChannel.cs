﻿using Iot.Device.OneWire;
using Leviathan.Channels.SDK;
using Leviathan.Services.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.OneWire {

	public interface ITempSensorService : ILeviathanService { }

	[Channel]
	public class TempSensorChannel : IAsyncInputChannel<TempReading> {

		OneWireThermometerDevice _device;
		ILogger<TempSensorChannel> _log;

		public string Id => _device.DeviceId;

		public TempSensorChannel(ILogger<TempSensorChannel> log, OneWireThermometerDevice device) =>
			(_log, _device) = (log, device);

		public async Task<TempReading> GetValueAsync() {
			var temp = await _device.ReadTemperatureAsync();
			return new TempReading {
				Celsius = temp.DegreesCelsius,
				Farenheit = temp.DegreesFahrenheit,
				Kelvin = temp.Kelvins
			};
		}

		async Task<object> IAsyncInputChannel.GetValueAsync() =>
			await this.GetValueAsync();
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