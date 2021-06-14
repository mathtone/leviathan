using Leviathan.DataAccess;
using System;
using System.Collections.Generic;

namespace Leviathan.SDK {

	[AttributeUsage(AttributeTargets.Class)]
	public class LeviathanComponentAttribute : Attribute {

		public string Name { get; }
		public string Description { get; }
		public string Category { get; }

		public LeviathanComponentAttribute(string name, string category, string description) {
			this.Name = name;
			this.Category = category;
			this.Description = description;
		}
	}
	public class HardwareComponentAttribute : LeviathanComponentAttribute {
		public HardwareComponentAttribute(string name, string description) : base(name, "Hardware Component", description) {
		}
	}

	public interface ILeviathanCorePartners {
		ISystemConfiguration Config { get; }
		ISystemDbData SystemData { get; }
		//IInstanceDbData InstanceData { get; }
		IComponentData PluginData { get; }
		IComponentCategoryData PluginCategoryData { get; }
		//IInitializationService Initialization { get; }
		//IHardwareService Hardware { get; }
	}

	public interface IComponentData : IListRepository<ComponentRecord> { }
	public interface IComponentCategoryData : IListRepository<Category> { }
	public interface IComponentService {
		IComponentData Plugins { get; }
		IComponentCategoryData Categories { get; }
		ComponentCatalog Catalog();
	}

	public class ComponentCatalog {
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<ComponentRecord> Registered { get; set; }
		public IEnumerable<ComponentDescriptor> Found { get; set; }
	}

	public class ComponentRecord {
		public long? Id { get; set; }
		public long? ComponentCategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public TypeRecord Type { get; set; }
		public bool IsRegistered => Id.HasValue;
	}

	public class ComponentDescriptor {
		public LeviathanComponentAttribute Attribute { get; set; }
		public Type Type { get; set; }
	}

	public class TypeRecord {
		public string TypeName { get; set; }
		public string AssemblyName { get; set; }
		public string AssemblyPath { get; set; }
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