using System.Device.I2c;
namespace Leviathan.Hardware.I2c {
	public class I2cDeviceConfig {
		public int Id { get; set; }
		public int ModuleId { get; set; }
		public I2cConnectionSettings Settings { get; set; }
	}
}
