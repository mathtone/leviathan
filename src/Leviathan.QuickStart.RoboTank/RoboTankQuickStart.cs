using Leviathan.Hardware;
using Leviathan.Hardware.Gpio;
using Leviathan.Hardware.PCA9685;
using Leviathan.Services.Core.Hardware;
using Leviathan.Services.Core.QuickStart;
using Newtonsoft.Json;
using System;
using System.Linq;
using static Newtonsoft.Json.JsonConvert;

namespace Leviathan.QuickStart.RoboTank {

	public class RoboTankQuickStart : IQuickStartProfile {

		IHardwareService hardware;

		public RoboTankQuickStart(IHardwareService hardware) {
			this.hardware = hardware;
		}

		public QuickStartInfo Info { get; } = new() {
			Id = "ROBO.TANK",
			Name = "Robo Tank",
			Description = "Configures hardware for Robo-Tank controller"
		};

		public void Apply() {

			var moduleTypes = new[] {
				new HardwareModuleTypeInfo(typeof(GpioModule).AssemblyQualifiedName ,"GPIO"),
				new HardwareModuleTypeInfo(typeof(PwmIOModule).AssemblyQualifiedName,"PCA9865")
			}.Select(hardware.ModuleTypes.Create).ToArray();

			var channelTypes = new[] {
				new ChannelTypeInfo(typeof(GpioChannel).AssemblyQualifiedName,"GPIO Channel"),
				new ChannelTypeInfo(typeof(PwmIOChannel).AssemblyQualifiedName,"PWM Channel")
			}.Select(hardware.ChannelTypes.Create).ToArray(); ;

			var controllerTypes = new[] {
				new ChannelControllerTypeInfo (typeof(GpioOnOffController).AssemblyQualifiedName,"GPIO On/Off"),
				new ChannelControllerTypeInfo (typeof(GpioSensorController).AssemblyQualifiedName,"GPIO Sensor"),
				new ChannelControllerTypeInfo (typeof(PwmVariableController).AssemblyQualifiedName,"PCA9685 PWM")
			}.Select(hardware.ChannelControllerTypes.Create).ToArray();

			var modules = new[] {
				new HardwareModuleInfo (moduleTypes[0].ModuleTypeId,"GPIO"),
				new HardwareModuleInfo (moduleTypes[1].ModuleTypeId,"PCA9685"),
			}.Select(hardware.Modules.Create).ToArray();

			var channels = new[] {
				//AC PORTS
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 1",SerializeObject(new{ Pin=9,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 2",SerializeObject(new{ Pin=10,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 3",SerializeObject(new{ Pin=22,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 4",SerializeObject(new{ Pin=15,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 5",SerializeObject(new{ Pin=14,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 6",SerializeObject(new{ Pin=23,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 7",SerializeObject(new{ Pin=24,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 8",SerializeObject(new{ Pin=25,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 9",SerializeObject(new{ Pin=16,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 10",SerializeObject(new{ Pin=6,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 11",SerializeObject(new{ Pin=5,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 12",SerializeObject(new{ Pin=7,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 13",SerializeObject(new{ Pin=11,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 14",SerializeObject(new{ Pin=8,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 15",SerializeObject(new{ Pin=12,Mode=1})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 16",SerializeObject(new{ Pin=13,Mode=1})),

				//SENSOR PORTS
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 17",SerializeObject(new{ Pin=17,Mode=0})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 18",SerializeObject(new{ Pin=27,Mode=0})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 19",SerializeObject(new{ Pin=19,Mode=0})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 20",SerializeObject(new{ Pin=20,Mode=0})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 21",SerializeObject(new{ Pin=26,Mode=0})),
				new ChannelInfo(modules[0].Id,channelTypes[0].Id,"GPIO 22",SerializeObject(new{ Pin=21,Mode=0})),

				//PWM CHANNELS
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 1",SerializeObject(new{ PwmChannelId=15})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 2",SerializeObject(new{ PwmChannelId=14})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 3",SerializeObject(new{ PwmChannelId=13})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 4",SerializeObject(new{ PwmChannelId=12})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 5",SerializeObject(new{ PwmChannelId=11})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 6",SerializeObject(new{ PwmChannelId=10})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 7",SerializeObject(new{ PwmChannelId=9})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 8",SerializeObject(new{ PwmChannelId=8})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 9",SerializeObject(new{ PwmChannelId=0})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 10",SerializeObject(new{ PwmChannelId=1})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 11",SerializeObject(new{ PwmChannelId=2})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 12",SerializeObject(new{ PwmChannelId=3})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 13",SerializeObject(new{ PwmChannelId=4})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 14",SerializeObject(new{ PwmChannelId=5})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 15",SerializeObject(new{ PwmChannelId=6})),
				new ChannelInfo(modules[1].Id,channelTypes[1].Id,"PWM 16",SerializeObject(new{ PwmChannelId=7}))
			}.Select(hardware.Channels.Create).ToArray(); ;

			var channelControllers = new[] {
				//AC Ports
				new ChannelControllerInfo("AC.01",controllerTypes[0].Id,channels[0].Id),
				new ChannelControllerInfo("AC.02",controllerTypes[0].Id,channels[1].Id),
				new ChannelControllerInfo("AC.03",controllerTypes[0].Id,channels[2].Id),
				new ChannelControllerInfo("AC.04",controllerTypes[0].Id,channels[3].Id),
				new ChannelControllerInfo("AC.05",controllerTypes[0].Id,channels[4].Id),
				new ChannelControllerInfo("AC.06",controllerTypes[0].Id,channels[5].Id),
				new ChannelControllerInfo("AC.07",controllerTypes[0].Id,channels[6].Id),
				new ChannelControllerInfo("AC.08",controllerTypes[0].Id,channels[7].Id),
				new ChannelControllerInfo("AC.09",controllerTypes[0].Id,channels[8].Id),
				new ChannelControllerInfo("AC.10",controllerTypes[0].Id,channels[9].Id),
				new ChannelControllerInfo("AC.11",controllerTypes[0].Id,channels[10].Id),
				new ChannelControllerInfo("AC.12",controllerTypes[0].Id,channels[11].Id),
				new ChannelControllerInfo("AC.13",controllerTypes[0].Id,channels[12].Id),
				new ChannelControllerInfo("AC.14",controllerTypes[0].Id,channels[13].Id),
				new ChannelControllerInfo("AC.15",controllerTypes[0].Id,channels[14].Id),
				new ChannelControllerInfo("AC.16",controllerTypes[0].Id,channels[15].Id),

				//SENSOR PORTS
				new ChannelControllerInfo("SS.01",controllerTypes[1].Id,channels[16].Id),
				new ChannelControllerInfo("SS.02",controllerTypes[1].Id,channels[17].Id),
				new ChannelControllerInfo("SS.03",controllerTypes[1].Id,channels[18].Id),
				new ChannelControllerInfo("SS.04",controllerTypes[1].Id,channels[19].Id),
				new ChannelControllerInfo("SS.05",controllerTypes[1].Id,channels[20].Id),
				new ChannelControllerInfo("SS.06",controllerTypes[1].Id,channels[21].Id),

				//DC PORTS
				new ChannelControllerInfo("DC.01",controllerTypes[2].Id,channels[22].Id),
				new ChannelControllerInfo("DC.02",controllerTypes[2].Id,channels[23].Id),
				new ChannelControllerInfo("DC.03",controllerTypes[2].Id,channels[24].Id),
				new ChannelControllerInfo("DC.04",controllerTypes[2].Id,channels[25].Id),
				new ChannelControllerInfo("DC.05",controllerTypes[2].Id,channels[26].Id),
				new ChannelControllerInfo("DC.06",controllerTypes[2].Id,channels[27].Id),
				new ChannelControllerInfo("DC.07",controllerTypes[2].Id,channels[28].Id),
				new ChannelControllerInfo("DC.0F",controllerTypes[2].Id,channels[29].Id),

				//PWM/ANALOG PORTS
				new ChannelControllerInfo("PA.01",controllerTypes[2].Id,channels[30].Id),
				new ChannelControllerInfo("PA.02",controllerTypes[2].Id,channels[31].Id),
				new ChannelControllerInfo("PA.03",controllerTypes[2].Id,channels[32].Id),
				new ChannelControllerInfo("PA.04",controllerTypes[2].Id,channels[33].Id),
				new ChannelControllerInfo("PA.05",controllerTypes[2].Id,channels[34].Id),
				new ChannelControllerInfo("PA.06",controllerTypes[2].Id,channels[35].Id),
				new ChannelControllerInfo("PA.07",controllerTypes[2].Id,channels[36].Id),
				new ChannelControllerInfo("PA.08",controllerTypes[2].Id,channels[37].Id)
																  

				
			}.Select(hardware.ChannelControllers.Create).ToArray(); ; ;
		}
	}
}