using Leviathan.Components;
using System;
using Iot.Device.Pwm;
using System.Device.I2c;
using Leviathan.Hardware.I2C;
using Leviathan.Hardware;
//using Leviathan.Hardware.I2c;

[assembly: LeviathanPlugin("PCA9685 Drivers")]
namespace Leviathan.Hardware.PCA9685 {

	[Driver("PCA9685", "PCA9685 Pwm Controller")]
	public class LeviathanPca9685 : IDeviceDriver<Pca9685, I2cDeviceSettings> {

		readonly LeviathanI2C i2c;

		public LeviathanPca9685(LeviathanI2C i2c) {
			this.i2c = i2c;
		}

		public Pca9685 CreateDevice(I2cDeviceSettings data) =>
			new(i2c.CreateDevice(data));
	}


	public class PwmIOConnectorData {

		public int PwmChannelId { get; init; }
	}

	[LeviathanConnector("PWM IO", "PCA9685 PWM Connector")]
	public class PwmIOConnector {
		public PwmIOConnector(LeviathanPca9685 device, PwmIOConnectorData connectorData) {
			;
		}
	}

	[LeviathanChannel("PWM 0/1", "PCA9685 On/Off")]
	public class PwmOnOffChannel : IInputChannel<bool>, IOutputChannel<bool> {
		public void SetValue(bool value) {
			throw new System.NotImplementedException();
		}

		public bool GetValue() {
			throw new System.NotImplementedException();
		}
	}


	[LeviathanChannel("PWM Sensor", "PCA9685 Input-only")]
	public class PwmSensorChannel : IInputChannel<double> {
		public double GetValue() {
			throw new System.NotImplementedException();
		}
	}

	[LeviathanChannel("PWM", "PCA9685 Input/Output")]
	public class PwmChannel : IInputChannel<double>, IOutputChannel<double> {

		public PwmChannel(PwmIOConnector connector) {
			;
		}

		public void SetValue(double value) { }
		public double GetValue() => 0;
	}

}
