using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Pwm;
using Leviathan.Hardware;
using Leviathan.Hardware.Gpio;
using Leviathan.Hardware.I2c;
using Leviathan.Hardware.PCA9685;

namespace Leviathan.TestConsole {

	public class LeviathanConfig {
		public HardwareModuleConfig[] HardwareModules { get; init; }
		public ChannelConfig[] Channels { get; init; }
		public ChannelControllerConfig[] ChannelControllers { get; set; }
	}

	public class TheLeviathan {

		readonly LeviathanConfig config;
		Dictionary<int, HardwareModule> modules;
		Dictionary<int, IChannel> channels;
		Dictionary<int, IChannel> channelControlers;

		public TheLeviathan(LeviathanConfig config) {
			this.config = config;
		}

		public Task Awaken() => Task.Run(() => {
			this.modules = config.HardwareModules.ToDictionary(c => c.ModuleId, c => CreateModule(c));
			this.channels = config.Channels.ToDictionary(c => c.ChannelId, c => CreateChannel(c));
			this.channelControlers = config.ChannelControllers.ToDictionary(c => c.Id, c => CreateController(c));
		});

		public Task Test() => Task.Run(() => {

			var c1 = this.channelControlers[1] as IInputOutputChannel<bool>;
			var c2 = this.channelControlers[2] as IInputOutputChannel<double>;
			var c3 = this.channelControlers[3] as IInputChannel<int>;

			c1.Value = true;
			c1.Value = false;

			c2.Value = 0.01;
			c2.Value = 0;

			var v = c3.Value;
		});

		public Task Sleep() => Task.Run(() => {
			DisposeOf(ref channelControlers);
			DisposeOf(ref channels);
			DisposeOf(ref modules);
		});

		protected static void DisposeOf<T, V>(ref Dictionary<T, V> values) {
			var v = values.Values.ToArray();
			values.Clear();
			values = null;
			foreach (var x in v) {
				if (x is IDisposable d)
					d.Dispose();
			}
		}

		protected static HardwareModule CreateModule(HardwareModuleConfig config) => Construct<HardwareModule>(config.ModuleType, config);
		protected IChannel CreateController(ChannelControllerConfig config) => Construct<IChannel>(config.ControllerType, this.channels[config.ChannelId], config);
		protected IChannel CreateChannel(ChannelConfig config) => Construct<IChannel>(config.ChannelType, this.modules[config.ModuleId], config);
		protected static T Construct<T>(Type type, params object[] parameters) => (T)type.GetConstructor(parameters.Select(p => p.GetType()).ToArray()).Invoke(parameters);
	}

	public class Program {

		static void Main(string[] args) {

			var cid = 1;
			var acPins = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13 };
			var sensorPins = new[] { 17, 27, 19, 20, 26, 21 };
			var pwmIds = new[] { 15, 14, 13, 12, 11, 10, 9, 8, 0, 1, 2, 3, 4, 5, 6, 7 };
			var cfg = new LeviathanConfig {

				HardwareModules = new[] {
					new HardwareModuleConfig { ModuleId = 1, Name = "GPIO", ModuleType = typeof(GpioModule) },
					new I2cModuleConfig {
						ModuleId = 2,
						Name = "PWM",
						ModuleType = typeof(PwmIOModule),
						Device = new I2cDeviceConfig {
							Id = 1,
							ModuleId = 2,
							Settings = new I2cConnectionSettings(1, Pca9685.I2cAddressBase + 0b000000)
						}
					}
				},

				Channels = acPins.Select(i => new GpioChannelConfig { ModuleId = 1, ChannelId = cid++, Mode = PinMode.Output, Pin = i, ChannelType = typeof(GpioChannel) }).Cast<ChannelConfig>()
					.Concat(sensorPins.Select(i => new GpioChannelConfig { ModuleId = 1, ChannelId = cid++, Mode = PinMode.Input, Pin = i, ChannelType = typeof(GpioChannel) }))
					.Concat(pwmIds.Select(i => new PwmIOChannelConfig { ModuleId = 2, ChannelId = cid++, PwmChannelId = i, ChannelType = typeof(PwmIOChannel) }))
					.ToArray(),

				ChannelControllers = new ChannelControllerConfig[] {

					new ChannelControllerConfig {
						Id=1,
						ChannelId = 5,
						Name = "AC 1",
						ControllerType = typeof(GpioOnOffController)
					},

					new ChannelControllerConfig {
						Id=2,
						ChannelId = 6,
						Name = "AC 2",
						ControllerType = typeof(GpioOnOffController)
					},

					new ChannelControllerConfig {
						Id=3,
						Name = "PWM 1",
						ChannelId = 26,
						ControllerType = typeof(PwmVariableController)
					},

					new ChannelControllerConfig {
						Id=4,
						ChannelId = 17,
						Name = "Sensor 1",
						ControllerType = typeof(GpioSensorController)
					},
				}
			};

			var leviathan = new TheLeviathan(cfg);
			leviathan.Awaken().Wait();
			leviathan.Test().Wait();
			leviathan.Sleep().Wait();
		}
	}
}