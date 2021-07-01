using Leviathan.Components;
using System.Device.Gpio;

namespace Leviathan.Hardware.RPIGPIO {
	[LeviathanChannel("GPIO Sensor", "GPIO Input-only")]
	public class GpioSensorChannel : IInputChannel<double> {

		private readonly GpioConnector _connector;
		private readonly PinMode _mode;

		public GpioSensorChannel(GpioConnector connector, GpioChannelData channelData) {
			_connector = connector;
			_mode = channelData.Mode;
		}

		public double GetValue() {
			throw new System.NotImplementedException();
		}
	}
}