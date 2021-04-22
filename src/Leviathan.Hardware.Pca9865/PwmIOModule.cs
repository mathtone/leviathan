using Iot.Device.Pwm;
using Leviathan.Hardware.I2c;

namespace Leviathan.Hardware.PCA9685 {
	public class PwmIOModule : I2cModule {

		public Pca9685 PcaDevice { get; }

		public PwmIOModule(I2cModuleConfig config) : base(config) {
			PcaDevice = new Pca9685(this.Device);
		}
	}
}