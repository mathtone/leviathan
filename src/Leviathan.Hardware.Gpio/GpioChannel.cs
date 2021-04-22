using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Hardware.Gpio {
	public class GpioChannelConfig : ChannelConfig {
		public int Pin { get; set; }
		public PinMode Mode { get; set; }
		//public string Name { get; set; }
	}

	public class GpioChannel : InputOutputChannel<PinValue, GpioModule, GpioChannelConfig> {

		public int Pin => Config.Pin;
		public PinMode Mode => Config.Mode;

		public override PinValue Value {
			get => Device.Controller.Read(Pin);
			set => Device.Controller.Write(Pin, value);
		}

		public GpioChannel(GpioModule device, GpioChannelConfig config) : base(device, config) {
			this.Device.Controller.OpenPin(Pin, Mode);
		}
	}
}