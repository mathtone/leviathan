using Leviathan.Hardware;
using Leviathan.Hardware.Gpio;
using Leviathan.Hardware.PCA9685;
using Leviathan.Services.Core.Hardware;
using Leviathan.Services.Core.QuickStart;
using System;
using System.Linq;

namespace Leviathan.QuickStart.Basic {
	public class BasicQuickStart : IQuickStartProfile {

		protected IHardwareService hardware;
		protected HardwareModuleTypeInfo[] moduleTypes;
		protected ChannelTypeInfo[] channelTypes;
		protected ChannelControllerTypeInfo[] controllerTypes;

		public virtual QuickStartInfo Info { get; } = new() {
			Id = "SO.BASIC",
			Name = "Basic Config",
			Description = "Configures assemblies & types, user muse configure all channels"
		};

		public BasicQuickStart(IHardwareService hardware) {
			this.hardware = hardware;
		}

		public virtual void Apply() {
			moduleTypes = new HardwareModuleTypeInfo[] {
				new(typeof(GpioModule).AssemblyQualifiedName ,"GPIO"),
				new(typeof(PwmIOModule).AssemblyQualifiedName,"PCA9865")
			}.Select(hardware.ModuleTypes.Create).ToArray();

			channelTypes = new ChannelTypeInfo[] {
				new (typeof(GpioChannel).AssemblyQualifiedName,"GPIO Channel"),
				new (typeof(PwmIOChannel).AssemblyQualifiedName,"PWM Channel")
			}.Select(hardware.ChannelTypes.Create).ToArray(); ;

			controllerTypes = new ChannelControllerTypeInfo[] {
				new (typeof(GpioOnOffController).AssemblyQualifiedName,"GPIO On/Off"),
				new (typeof(GpioSensorController).AssemblyQualifiedName,"GPIO Sensor"),
				new (typeof(PwmVariableController).AssemblyQualifiedName,"PCA9685 PWM")
			}.Select(hardware.ChannelControllerTypes.Create).ToArray();
		}
	}
}