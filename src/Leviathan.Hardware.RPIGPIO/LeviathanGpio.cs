using Leviathan.Components;
using Leviathan.Hardware;
using System.Device.Gpio;
[assembly: LeviathanPlugin("GPIO Drivers")]
namespace Leviathan.Hardware.RPIGPIO {

	public class GpioDeviceData {
		public PinNumberingScheme PinNumberingScheme { get; init; }
	}

	[Driver("GPIO", "General purpose in/out")]
	public class LeviathanGpio : IDeviceDriver<GpioController, GpioDeviceData>, IDeviceDriver<GpioController> {

		public LeviathanGpio() { }
		public GpioController CreateDevice(GpioDeviceData data = null) => new(data?.PinNumberingScheme ?? PinNumberingScheme.Logical);
		public GpioController CreateDevice() => CreateDevice(null);
	}

	public class GpioConnectorData {

		public int Pin { get; init; }
		public int Mode { get; init; }
	}

	[LeviathanConnector("GPIO PIN", "Gpio Connector")]
	public class GpioConnector {
		public GpioConnector(GpioController device, GpioConnectorData connectorData) {
			;
		}
	}

	[LeviathanChannel("GPIO 0/1", "GPIO On/Off")]
	public class GpioOnOffChannel : IInputChannel<bool>, IOutputChannel<bool> {
		public void SetValue(bool value) {
			throw new System.NotImplementedException();
		}

		public bool GetValue() {
			throw new System.NotImplementedException();
		}
	}


	[LeviathanChannel("GPIO Sensor", "GPIO Input-only")]
	public class GpioSensorChannel : IInputChannel<double> {
		public double GetValue() {
			throw new System.NotImplementedException();
		}
	}

	[LeviathanChannel("GPIO", "GPIO Input/Output")]
	public class GpioChannel : IInputChannel<double>, IOutputChannel<double> {

		public GpioChannel(GpioConnector connector, PinMode mode) {
			;
		}

		public void SetValue(double value) { }
		public double GetValue() => 0;
	}

	
}