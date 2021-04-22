using System;
using System.Collections.Generic;
using System.Device.Pwm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Pwm;

namespace Leviathan.Hardware.PCA9685 {

	public class PwmIOChannelConfig : ChannelConfig {
		public int PwmChannelId { get; set; }
	}

	public class PwmIOChannel : InputOutputChannel<double, PwmIOModule, PwmIOChannelConfig> {
		public int PwmChannelId { get; }
		public PwmChannel PwmChannel { get; }

		public override double Value {
			get => PwmChannel.DutyCycle;
			set => PwmChannel.DutyCycle = value;
		}

		public PwmIOChannel(PwmIOModule device, PwmIOChannelConfig config) :
			base(device, config) {
			this.PwmChannel = device.PcaDevice.CreatePwmChannel(this.PwmChannelId = config.PwmChannelId);
		}

		protected override void OnDispose() {
			base.OnDispose();
			PwmChannel.Dispose();
		}
	}
}