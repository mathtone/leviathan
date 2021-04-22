using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Hardware.Gpio {
	public class GpioOnOffController : InputOutputChannelController<bool, GpioChannel, ChannelControllerConfig> {
		//public override PwmIOChannel Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override bool Value {
			get => Channel.Value == PinValue.High;
			set => Channel.Value = value ? PinValue.High : PinValue.Low;
		}
		public GpioOnOffController(GpioChannel channel, ChannelControllerConfig config) : base(channel, config) {
		}
	}

	public class GpioSensorController : InputChannelController<int, GpioChannel, ChannelControllerConfig> {
		public override int Value => (int)Channel.Value;
		public GpioSensorController(GpioChannel channel, ChannelControllerConfig config) : base(channel, config) {
		}
	}
}