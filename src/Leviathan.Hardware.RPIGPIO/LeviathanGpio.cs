using Leviathan.Components;
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
}