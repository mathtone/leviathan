using Leviathan.Hardware;
using Leviathan.Hardware.Gpio;
using Leviathan.Hardware.PCA9685;
using Leviathan.QuickStart.Basic;
using Leviathan.Services.Core.Hardware;
using Leviathan.Services.Core.QuickStart;
using Newtonsoft.Json;
using System;
using System.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace Leviathan.QuickStart.RoboTank {

	public class RoboTankQuickStart : BasicQuickStart {

		protected ChannelInfo[] channels;
		protected HardwareModuleInfo[] modules;
		protected ChannelControllerInfo[] channelControllers;

		public RoboTankQuickStart(IHardwareService hardware) : base(hardware) { }

		public override QuickStartInfo Info { get; } = new() {
			Id = "ROBO.TANK",
			Name = "Robo Tank",
			Description = "Configures hardware for Robo-Tank controller"
		};

		public override void Apply() {
			//Apply the basic quickstart
			base.Apply();

			modules = new HardwareModuleInfo[] {
				new(moduleTypes[0].ModuleTypeId,"GPIO"),
				new(moduleTypes[1].ModuleTypeId,"PCA9685"),
			}.Select(hardware.Modules.Create).ToArray();

			var i = 1;
			var gpio = new[] { 9, 10, 22, 15, 14, 23, 24, 25, 16, 6, 5, 7, 11, 8, 12, 13 }.Select(p =>
				new ChannelInfo(
					modules[0].Id,
					channelTypes[0].Id,
					$"GPIO {i++}",
					SerializeObject(new { Pin = p, Mode = 1 })
				)
			);

			var sensors = new[] { 17, 27, 19, 20, 26, 21 }.Select(p =>
				new ChannelInfo(
					modules[0].Id,
					channelTypes[0].Id,
					$"GPIO {i++}",
					SerializeObject(new { Pin = p, Mode = 0 })
				)
			);

			i = 1;
			var pwm = new[] { 15, 14, 13, 12, 11, 10, 9, 8, 0, 1, 2, 3, 4, 5, 6, 7 }.Select(p =>
				new ChannelInfo(
					modules[1].Id,
					channelTypes[1].Id,
					$"PWM {i++}",
					SerializeObject(new { PwmChannelId = p })
				)
			);

			channels = gpio
				.Concat(sensors)
				.Concat(pwm)
				.Select(hardware.Channels.Create)
				.ToArray();


			//channels = new ChannelInfo[] {
			//	//AC PORTS
			//	new(modules[0].Id,c0,"GPIO 1",SerializeObject(new{ Pin=9,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 2",SerializeObject(new{ Pin=10,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 3",SerializeObject(new{ Pin=22,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 4",SerializeObject(new{ Pin=15,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 5",SerializeObject(new{ Pin=14,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 6",SerializeObject(new{ Pin=23,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 7",SerializeObject(new{ Pin=24,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 8",SerializeObject(new{ Pin=25,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 9",SerializeObject(new{ Pin=16,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 10",SerializeObject(new{ Pin=6,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 11",SerializeObject(new{ Pin=5,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 12",SerializeObject(new{ Pin=7,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 13",SerializeObject(new{ Pin=11,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 14",SerializeObject(new{ Pin=8,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 15",SerializeObject(new{ Pin=12,Mode=1})),
			//	new(modules[0].Id,c0,"GPIO 16",SerializeObject(new{ Pin=13,Mode=1})),

			//	//SENSOR PORTS
			//	new(modules[0].Id,c0,"GPIO 17",SerializeObject(new{ Pin=17,Mode=0})),
			//	new(modules[0].Id,c0,"GPIO 18",SerializeObject(new{ Pin=27,Mode=0})),
			//	new(modules[0].Id,c0,"GPIO 19",SerializeObject(new{ Pin=19,Mode=0})),
			//	new(modules[0].Id,c0,"GPIO 20",SerializeObject(new{ Pin=20,Mode=0})),
			//	new(modules[0].Id,c0,"GPIO 21",SerializeObject(new{ Pin=26,Mode=0})),
			//	new(modules[0].Id,c0,"GPIO 22",SerializeObject(new{ Pin=21,Mode=0})),

			//	//PWM CHANNELS
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 1",SerializeObject(new{ PwmChannelId=15})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 2",SerializeObject(new{ PwmChannelId=14})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 3",SerializeObject(new{ PwmChannelId=13})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 4",SerializeObject(new{ PwmChannelId=12})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 5",SerializeObject(new{ PwmChannelId=11})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 6",SerializeObject(new{ PwmChannelId=10})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 7",SerializeObject(new{ PwmChannelId=9})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 8",SerializeObject(new{ PwmChannelId=8})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 9",SerializeObject(new{ PwmChannelId=0})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 10",SerializeObject(new{ PwmChannelId=1})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 11",SerializeObject(new{ PwmChannelId=2})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 12",SerializeObject(new{ PwmChannelId=3})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 13",SerializeObject(new{ PwmChannelId=4})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 14",SerializeObject(new{ PwmChannelId=5})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 15",SerializeObject(new{ PwmChannelId=6})),
			//	new(modules[1].Id,channelTypes[1].Id,"PWM 16",SerializeObject(new{ PwmChannelId=7}))
			//}.Select(hardware.Channels.Create).ToArray(); ;

			channelControllers = new ChannelControllerInfo[] {
				//AC Ports
				new("AC.01",controllerTypes[0].Id,channels[0].Id),
				new("AC.02",controllerTypes[0].Id,channels[1].Id),
				new("AC.03",controllerTypes[0].Id,channels[2].Id),
				new("AC.04",controllerTypes[0].Id,channels[3].Id),
				new("AC.05",controllerTypes[0].Id,channels[4].Id),
				new("AC.06",controllerTypes[0].Id,channels[5].Id),
				new("AC.07",controllerTypes[0].Id,channels[6].Id),
				new("AC.08",controllerTypes[0].Id,channels[7].Id),
				new("AC.09",controllerTypes[0].Id,channels[8].Id),
				new("AC.10",controllerTypes[0].Id,channels[9].Id),
				new("AC.11",controllerTypes[0].Id,channels[10].Id),
				new("AC.12",controllerTypes[0].Id,channels[11].Id),
				new("AC.13",controllerTypes[0].Id,channels[12].Id),
				new("AC.14",controllerTypes[0].Id,channels[13].Id),
				new("AC.15",controllerTypes[0].Id,channels[14].Id),
				new("AC.16",controllerTypes[0].Id,channels[15].Id),

				//SENSOR PORTS
				new("SS.01",controllerTypes[1].Id,channels[16].Id),
				new("SS.02",controllerTypes[1].Id,channels[17].Id),
				new("SS.03",controllerTypes[1].Id,channels[18].Id),
				new("SS.04",controllerTypes[1].Id,channels[19].Id),
				new("SS.05",controllerTypes[1].Id,channels[20].Id),
				new("SS.06",controllerTypes[1].Id,channels[21].Id),

				//DC PORTS
				new("DC.01",controllerTypes[2].Id,channels[22].Id),
				new("DC.02",controllerTypes[2].Id,channels[23].Id),
				new("DC.03",controllerTypes[2].Id,channels[24].Id),
				new("DC.04",controllerTypes[2].Id,channels[25].Id),
				new("DC.05",controllerTypes[2].Id,channels[26].Id),
				new("DC.06",controllerTypes[2].Id,channels[27].Id),
				new("DC.07",controllerTypes[2].Id,channels[28].Id),
				new("DC.0F",controllerTypes[2].Id,channels[29].Id),

				//PWM/ANALOG PORTS
				new("PA.01",controllerTypes[2].Id,channels[30].Id),
				new("PA.02",controllerTypes[2].Id,channels[31].Id),
				new("PA.03",controllerTypes[2].Id,channels[32].Id),
				new("PA.04",controllerTypes[2].Id,channels[33].Id),
				new("PA.05",controllerTypes[2].Id,channels[34].Id),
				new("PA.06",controllerTypes[2].Id,channels[35].Id),
				new("PA.07",controllerTypes[2].Id,channels[36].Id),
				new("PA.08",controllerTypes[2].Id,channels[37].Id)
			}.Select(hardware.ChannelControllers.Create).ToArray(); ; ;
		}
	}
}