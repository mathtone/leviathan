using Iot.Device.Pwm;
using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.Hardware;
using Leviathan.Services;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.I2c;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iot.Device.Adc;
using System.Device.Spi;
using System.Threading;

namespace Sandbox.TestConsole {
	class Program {
		static async Task<int> Main(string[] args) {

			var connections = new InstanceConnectService(() => new NpgsqlConnection(DbConnectionString("poseidonalpha.local", "pi", "Digital!2021", "leviathan_alpha_0x00")));
			var Provider = new LeviathanAlphaDataContextProvider(connections);
			var ctx = Provider.CreateContext<NpgsqlConnection>();

			await ctx.Connection.OpenAsync();

			var gpioDevice = new GpioController(PinNumberingScheme.Logical);
			var pwmDevice = I2cDevice.Create(new I2cConnectionSettings(1, Pca9685.I2cAddressBase + 0b000000));
			var pca = new Pca9685(pwmDevice);

			var hardwareSpiSettings = new SpiConnectionSettings(0, 0);
			using SpiDevice spi = SpiDevice.Create(hardwareSpiSettings);
			using var mcp = new Mcp3008(spi);
			while (true) {
				double value = mcp.Read(0);
				value /= 10.24;
				value = Math.Round(value);
				Console.WriteLine($"{value}%");
				Thread.Sleep(500);
			}

			/*
			GPIO:
				AC Ports
				Sensor(s)
			PCA9685
				DC Ports
				PWM/A
			PH
			*/

			gpioDevice.OpenPin(11, PinMode.Output);
			gpioDevice.Write(11, PinValue.High);
			gpioDevice.Write(11, PinValue.Low);

			pca.SetDutyCycleAllChannels(1);
			pca.SetDutyCycleAllChannels(0);

			return 0;
		}

		static string DbConnectionString(string hostName, string login, string password, string database) =>
			$"Host={hostName};Username={login};Database={database};Password={password};";
	}

	public class GPIOChannelData { }

	public class GPIOChannel {
		public GPIOChannel(GPIOChannelData data) { }
	}

	public class PCA9685ChannelData { }

	public class PCA9685Channel {
		public PCA9685Channel(PCA9685ChannelData data) {
		}
	}
}