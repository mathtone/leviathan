using Leviathan.Components;
using System;
using Iot.Device.Pwm;
using System.Device.I2c;
using Leviathan.Hardware.I2C;
//using Leviathan.Hardware.I2c;

[assembly: LeviathanPlugin("PCA9685 Drivers")]
namespace Leviathan.Hardware.PCA9685 {

	[Driver("PCA9685", "PCA9685 Pwm Controller")]
	public class Pca9685Driver : IDeviceDriver<Pca9685, I2cDeviceSettings> {

		readonly I2CDriver i2c;

		public Pca9685Driver(I2CDriver i2c) {
			this.i2c = i2c;
		}

		public Pca9685 CreateDevice(I2cDeviceSettings data) => new(i2c.CreateDevice(data));
	}

	//[Driver("PCA9685", "PCA9685 PWM Controller")]
	//public class PCA9685Driver {
	//	public Pca9685 Device { get; }
	//}

	//public class I2cDriver {

	//	public I2cDevice Device { get; }

	//	public I2cDriver() {
	//		this.Device = I2cDevice.Create(config.Device.Settings);
	//		//Devices = config.ToDictionary(s => s.Id, s => I2cDevice.Create(s.Settings));
	//	}

	//	protected override void OnDispose() {
	//		base.OnDispose();
	//		Device.Dispose();
	//	}
	//}
}
