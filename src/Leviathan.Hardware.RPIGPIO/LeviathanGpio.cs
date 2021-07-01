using Leviathan.Components;
using Leviathan.Hardware;
using System.Device.Gpio;

namespace Leviathan.Hardware.RPIGPIO {

	public class GpioDeviceData {
		public PinNumberingScheme PinNumberingScheme { get; init; }
	}

	public class GpioChannelData {

		public PinMode Mode { get; init; }
	}


	[Driver("GPIO", "General purpose in/out")]
	public class LeviathanGpio : IDeviceDriver<GpioController, GpioDeviceData>, IDeviceDriver<GpioController> {

		//GpioDeviceData _deviceData;

		public LeviathanGpio() { }

		public GpioController CreateDevice(GpioDeviceData data = null) =>
			new(data?.PinNumberingScheme ?? PinNumberingScheme.Logical);

		public GpioController CreateDevice() => CreateDevice(null);

		object IDeviceDriver.CreateDevice(object data) => CreateDevice((GpioDeviceData)data);
	}
}