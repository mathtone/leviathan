using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.I2c;
namespace Leviathan.Hardware.I2c {

	public class I2cModuleConfig : HardwareModuleConfig {
		public I2cDeviceConfig Device { get; set; }
	}

	public class I2cModule : HardwareModule {

		public I2cDevice Device { get; }

		public I2cModule(I2cModuleConfig config) {
			this.Device = I2cDevice.Create(config.Device.Settings);
			//Devices = config.ToDictionary(s => s.Id, s => I2cDevice.Create(s.Settings));
		}

		protected override void OnDispose() {
			base.OnDispose();
			Device.Dispose();
		}
	}
}