using Leviathan.Components;
using System.Device.Gpio;

[assembly: LeviathanModule("Raspberry Pi GPIO")]
namespace Leviathan.Hardware.RPIGPIO {

	[LeviathanChannel("GPIO", "GPIO Input/Output")]
	public class GpioChannel : IInputChannel<double>, IOutputChannel<double> {

		private readonly GpioConnector _connector;
		private readonly PinMode _mode;

		public GpioChannel(GpioConnector connector, GpioChannelData channelData) {
			_connector = connector;
			_mode = channelData.Mode;
		}

		public void SetValue(double value) { }
		public double GetValue() => 0;
	}
}