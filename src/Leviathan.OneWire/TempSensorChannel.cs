using Iot.Device.OneWire;
using Leviathan.Channels.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.OneWire {

	public class TempSensorChannel : IAsyncInputChannel<TempReading> {

		TempSensorChannelData _channelData;
		OneWireThermometerDevice _device;

		public TempSensorChannel(TempSensorChannelData data) {
			this._channelData = data;
			this._device = new OneWireThermometerDevice(_channelData.BusId, _channelData.DeviceId);
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