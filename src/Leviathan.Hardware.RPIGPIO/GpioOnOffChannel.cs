using Leviathan.Components;
using System.Device.Gpio;

namespace Leviathan.Hardware.RPIGPIO {

	[LeviathanChannel("GPIO 0/1", "GPIO On/Off")]
	public class GpioOnOffChannel : IInputChannel<bool>, IOutputChannel<bool> {

		private readonly GpioConnector _connector;
		private readonly PinMode _mode;

		public GpioOnOffChannel(GpioConnector connector, GpioChannelData channelData) {
			_connector = connector;
			_mode = channelData.Mode;
			_connector.Device.OpenPin(_connector.ConnectorData.Pin, _mode);
		}

		public void SetValue(bool value) {
			_connector.Device.Write(_connector.ConnectorData.Pin, value ? PinValue.High : PinValue.Low);
		}

		public bool GetValue() {
			return _connector.Device.Read(_connector.ConnectorData.Pin) != PinValue.Low;
		}
	}
}