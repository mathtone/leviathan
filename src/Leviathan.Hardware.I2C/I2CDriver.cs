using Leviathan.Components;
using System;
using System.Device.I2c;

[assembly: LeviathanPlugin("I2C Drivers")]

namespace Leviathan.Hardware.I2C {
	public class I2cDeviceSettings {
		public I2cConnectionSettings Settings { get; set; }
	}

	[Driver("I2C", "I2C (Inter-Integrated Circuit) Protocol driver")]
	public class I2CDriver : IDeviceDriver<I2cDevice, I2cDeviceSettings> {
		public I2cDevice CreateDevice(I2cDeviceSettings settings) => I2cDevice.Create(settings.Settings);
	}
}