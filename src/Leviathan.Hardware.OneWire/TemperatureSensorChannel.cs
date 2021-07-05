using Leviathan.Components;
using Leviathan.Hardware;
using Iot.Device.OneWire;
using System;
using System.Threading.Tasks;
using UnitsNet;

[assembly: LeviathanPlugin("Raspberry Pi OneWire")]
namespace Leviathan.Hardware.OneWire {
	public class TemperatureSensorChannelData {
		public string BusId { get; init; }
		public string DeviceId { get; init; }
	}
	public class TempReading {
		public double Celsius { get; init; }
		public double Farenheit { get; init; }
		public double Kelvin { get; init; }
	}

	[LeviathanChannel("Temp", "OnwWire temperature sensor")]
	public class TemperatureSensorChannel : IInputChannel<Task<TempReading>> {
		TemperatureSensorChannelData _channelData;
		OneWireThermometerDevice _device;

		public TemperatureSensorChannel(TemperatureSensorChannelData data) {
			this._channelData = data;
			_device = new OneWireThermometerDevice(_channelData.BusId, _channelData.DeviceId);
		}

		public async Task<TempReading> GetValue() {
			var temp = await _device.ReadTemperatureAsync();
			return new TempReading {
				Celsius = temp.DegreesCelsius,
				Farenheit = temp.DegreesFahrenheit,
				Kelvin = temp.Kelvins
			};
		}
	}
}