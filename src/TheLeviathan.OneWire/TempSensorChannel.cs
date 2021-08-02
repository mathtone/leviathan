using Iot.Device.OneWire;
using Leviathan.Channels.Sdk;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnitsNet;
using static Newtonsoft.Json.JsonConvert;
namespace TheLeviathan.OneWire {
	public class TempSensorChannel : OneWireChannel, IAsyncInputChannel<Temperature> {
		OneWireThermometerDevice _device;

		public TempSensorChannel(IOneWireService service, string channelData) : this(service, DeserializeObject<OneWireChannelData>(channelData)) { }
		public TempSensorChannel(IOneWireService service, OneWireChannelData channelData) : base(service, channelData) {
			_device = new OneWireThermometerDevice(channelData.BusId, channelData.DeviceId);
		}

		public async Task<Temperature> GetAsync() => await _device.ReadTemperatureAsync();

		async Task<string> IAsyncInputChannel.GetAsync() {
			return (await GetAsync()).ToString();
		}
	}
}