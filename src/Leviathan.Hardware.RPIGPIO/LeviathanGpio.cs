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

		//GpioDeviceData _deviceData;

		public LeviathanGpio() {
			
		}

		public GpioController CreateDevice(GpioDeviceData data = null) =>
			new(data?.PinNumberingScheme ?? PinNumberingScheme.Logical);

		public GpioController CreateDevice() => CreateDevice(null);

		object IDeviceDriver.CreateDevice(object data) =>
			CreateDevice((GpioDeviceData)data);
	}

	public class GpioConnectorData {

		public int Pin { get; init; }
		public int Mode { get; init; }
	}

	[LeviathanConnector("GPIO PIN", "Gpio Connector")]
	public class GpioConnector {
		
		public GpioConnectorData ConnectorData { get; }
		protected GpioController Device { get; }

		public GpioConnector(GpioController device, GpioConnectorData connectorData) {
			this.Device = device;
			this.ConnectorData = connectorData;
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
		private readonly GpioConnector _connector;
		private readonly PinMode _mode;

		public GpioChannel(GpioConnector connector, PinMode mode) {
			_connector = connector;
			_mode = mode;
			
		}

		public void SetValue(double value) { }
		public double GetValue() => 0;
	}
}