using System.Device.Gpio;

namespace Leviathan.Hardware.Gpio {
	public class GpioModule : HardwareModule {

		public GpioController Controller { get; }

		public GpioModule(HardwareModuleConfig config) {
			this.Controller = new GpioController();
		}
	}


}