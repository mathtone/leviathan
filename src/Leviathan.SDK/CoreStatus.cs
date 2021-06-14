namespace Leviathan.SDK {
	public record CoreStatus {
		public bool DbCreated { get; set; }
		public bool Initialized { get; set; }
		public bool Running { get; set; }
	}
	//public interface IConfigurationProfile {
	//	public void Apply(ILeviathanCore core);
	//}

	//public abstract class ConfigurationProfile : IConfigurationProfile {
	//	public abstract void Apply(ILeviathanCore core);
	//}

	//[ConfigProfile("Robo Tank", "Configure the Leviathan for the Robo-Tank controller")]
	//public class RoboTankProfile : BasicProfile {
	//	public override void Apply(ILeviathanCore core) {
	//		base.Apply(core);
	//		//core.Hardware.HardwareModuleTypes.Create(new TypeInfo)
	//		//var modules = new HardwareModule[] {
	//		//	//new(moduleTypes[0].ModuleTypeId,"GPIO"),
	//		//	//new(moduleTypes[1].ModuleTypeId,"PCA9685"),
	//		//}.Select(core..Modules.Create).ToArray();
	//		//;
	//	}
	//}

	//[ConfigProfile("Basic", "Add types for included hardware modules, connectors & channels but does not configure them.")]
	//public class BasicProfile : ConfigurationProfile {
	//	public override void Apply(ILeviathanCore core) {
	//		core.PluginCategoryData.Create(new Category { Name = "Hardware Module", Description = "Hardware components & drivers" });
	//		core.PluginCategoryData.Create(new Category { Name = "Connector", Description = "Component connectors (ie: 'GPIO')" });
	//		core.PluginCategoryData.Create(new Category { Name = "Controller", Description = "Control channels (ie: 'PWM Variable')" });

	//		//locate all plugin components
	//	}
	//}

	//[ConfigProfile("Hardcore Mode", "Do nothing.  I will awaken the leviathan myself.")]
	//public class HardcoreProfile : ConfigurationProfile {
	//	public override void Apply(ILeviathanCore core) {

	//	}
	//}
}