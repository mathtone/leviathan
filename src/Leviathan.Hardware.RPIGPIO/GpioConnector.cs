using Leviathan.Components;
using System.Device.Gpio;

namespace Leviathan.Hardware.RPIGPIO {

	public class GpioConnectorData {

		public int Pin { get; init; }
		public int Mode { get; init; }
	}

	[LeviathanConnector("GPIO PIN", "Gpio Connector")]
	public class GpioConnector {

		public GpioConnectorData ConnectorData { get; }
		public GpioController Device { get; }

		public GpioConnector(GpioController device, GpioConnectorData connectorData) {
			this.Device = device;
			this.ConnectorData = connectorData;
		}
	}
}