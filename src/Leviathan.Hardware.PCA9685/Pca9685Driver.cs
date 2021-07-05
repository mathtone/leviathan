using Leviathan.Components;
using System;
using Iot.Device.Pwm;
using System.Device.I2c;
using Leviathan.Hardware.I2C;
using Leviathan.Hardware;
using System.Device.Pwm;
//using Leviathan.Hardware.I2c;

[assembly: LeviathanModule("PCA9685 Drivers")]
namespace Leviathan.Hardware.PCA9685 {

	[Driver("PCA9685", "PCA9685 Pwm Controller")]
	public class LeviathanPca9685 : IDeviceDriver<Pca9685, I2cDeviceSettings> {

		public LeviathanPca9685() { }
		public Pca9685 CreateDevice(I2cDeviceSettings data) =>
			new(I2cDevice.Create(data.Settings));

		object IDeviceDriver.CreateDevice(object data) =>
			CreateDevice((I2cDeviceSettings)data);
	}


	public class PwmIOConnectorData {

		public int PwmChannelId { get; init; }
	}

	[LeviathanConnector("PWM IO", "PCA9685 PWM Connector")]
	public class PwmIOConnector {

		Pca9685 _device;
		PwmIOConnectorData _connectorData;
		public System.Device.Pwm.PwmChannel Channel { get; }

		public PwmIOConnector(Pca9685 device, PwmIOConnectorData connectorData) {
			this._device = device;
			this._connectorData = connectorData;
			Channel = device.CreatePwmChannel(connectorData.PwmChannelId);
		}
	}

	[LeviathanChannel("PWM 0/1", "PCA9685 On/Off")]
	public class PwmOnOffChannel : IInputChannel<bool>, IOutputChannel<bool> {
		PwmIOConnector _connector;

		public PwmOnOffChannel(PwmIOConnector connector, object channeldata) {
			this._connector = connector;
		}


		public void SetValue(bool value) {
			throw new System.NotImplementedException();
		}

		public bool GetValue() {
			throw new System.NotImplementedException();
		}
	}

	[LeviathanChannel("PWM Sensor", "PCA9685 Input-only")]
	public class PwmSensorChannel : IInputChannel<double> {
		PwmIOConnector _connector;
		public PwmSensorChannel(PwmIOConnector connector, object channeldata) {
			this._connector = connector;
		}

		public double GetValue() {
			throw new System.NotImplementedException();
		}
	}

	[LeviathanChannel("PWM", "PCA9685 Input/Output")]
	public class PwmChannel : IInputChannel<double>, IOutputChannel<double> {

		PwmIOConnector _connector;
		public PwmChannel(PwmIOConnector connector, object channeldata) {
			this._connector = connector;
		}

		public void SetValue(double value) => this._connector.Channel.DutyCycle = value;
		public double GetValue() => this._connector.Channel.DutyCycle;
	}
}