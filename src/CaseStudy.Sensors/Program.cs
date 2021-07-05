using System;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using Iot.Device.OneWire;
using Iot.Device.Spi;
using Rinsen.IoT.OneWire;

namespace CaseStudy.Sensors {
	class Program {

		static async Task Main(string[] args) {
			foreach(var id in OneWireThermometerDevice.EnumerateDeviceIds()) {
				var bus = id.BusId;
				var dev = id.DevId;
				var therm = new OneWireThermometerDevice(bus, dev);
				var temp = await therm.ReadTemperatureAsync();
				;
			}


			//foreach (var busId in OneWireBus.EnumerateBusIds()) {

			//	var bus = new OneWireBus(busId);
			//	await bus.ScanForDeviceChangesAsync();

			//	foreach (string devId in bus.EnumerateDeviceIds()) {
			//		OneWireDevice dev = new(busId, devId);
			//		Console.WriteLine($"Found family '{dev.Family}' device '{dev.DeviceId}' on '{bus.BusId}'");
			//		if (OneWireThermometerDevice.IsCompatible(busId, devId)) {
			//			OneWireThermometerDevice devTemp = new(busId, devId);
			//			Console.WriteLine("Temperature reported by device: " + (await devTemp.ReadTemperatureAsync()).DegreesCelsius.ToString("F2") + "\u00B0C");
			//		}
			//	}
			//}
		}
	}
}