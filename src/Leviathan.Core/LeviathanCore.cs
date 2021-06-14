using Leviathan.DataAccess;
using Leviathan.Hardware;
using Leviathan.Plugins;
using Leviathan.SDK;
using Leviathan.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Leviathan.Core {



	public class InitializationEventArgs : EventArgs { }
	public class FactoryResetArgs : InitializationEventArgs { }
	public class ConfigurationAppliedArgs : InitializationEventArgs {
		public ISystemConfiguration Config { get; }

		public ConfigurationAppliedArgs(ISystemConfiguration config) {
			this.Config = config;
		}
	}


	public class LeviathanCore : ILeviathanCore {

		CoreStatus Status { get; } = new CoreStatus();
		public IDbConnectionProvider InstanceConnection { get; }
		public ISystemConfiguration Config { get; protected set; }
		public ISystemDbData SystemData { get; }
		public IComponentData PluginData { get; }
		public IComponentCategoryData PluginCategoryData { get; }

		public LeviathanCore(IDbConnectionProvider instanceData, ISystemConfiguration config, ISystemDbData systemDb, IComponentData pluginData, IComponentCategoryData pluginCategoryData) {

			this.Config = config;
			this.SystemData = systemDb;
			this.PluginData = pluginData;
			this.PluginCategoryData = pluginCategoryData;
			this.InstanceConnection = instanceData;
		}

		public void FactoryReset() {
			if (SystemData.LocateDB(Config.InstanceName)) {
				SystemData.DropDB(Config.InstanceName);
			}
		}

		public void Start() {
			this.Status.DbCreated = SystemData.LocateDB(Config.InstanceName);
			if (Status.DbCreated) {
				InstanceConnection.SetConnectionInfo($"Host={Config.HostName};Username={Config.DbLogin};Database={Config.InstanceName};Password={Config.DbPassword};");
			}
			this.Status.Running = true;
		}

		public void Stop() {
			this.Status.Running = true;
		}

		public void Restart() {
			Stop();
			Start();
		}

		public CoreStatus CurrentStatus() => this.Status with
		{
			DbCreated = SystemData.LocateDB(Config.InstanceName)
		};

		public void Configure(SystemConfiguration config) {

			//Core.Config = config;
			SystemData.CreateDB(Config.InstanceName);
			//(_ = ConfigurationApplied)?.Invoke(this, new ConfigurationAppliedArgs(Config));

			//if (Core.SystemDbData.LocateDB(config.DbName)) {
			//	;
			//}
			//else {
			//	Core.SystemDbData.CreateDB(config.DbName);
			//}

			//Create Db;
		}

		public IEnumerable<ConfigurationProfileListing> ListProfiles() =>
			GetLocalAssemblies()
				.SelectMany(a => a.DefinedTypes)
				.Select(t => new {
					Description = t.GetCustomAttribute<ConfigProfileAttribute>(),
					Type = t
				})
				.Where(r => r.Description != null)
				.Select(r => new ConfigurationProfileListing {
					Name = r.Description.ProfileName,
					Description = r.Description.Description,
					Type = r.Type.FullName
				});

		public void ApplyProfile(string configName) {
			var listing = ListProfiles().Single(p => p.Name == configName);
			var profile = (IConfigurationProfile)Activator.CreateInstance(Type.GetType(listing.Type));

			profile.Apply(this);
		}

		static IEnumerable<Assembly> GetLocalAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

		public ISystemConfiguration CurrentConfig() => this.Config;

	}


	[ConfigProfile("Robo Tank", "Configure the Leviathan for the Robo-Tank controller")]
	public class RoboTankProfile : ConfigurationProfile {
		public override void Apply(ILeviathanCore core) {
			var moduleCategories = new Category[] {
				new Category { Name = "Hardware Module", Description = "Hardware components & drivers" }
			}.Select(core.PluginCategoryData.Create).ToArray();

			var modules = new ComponentRecord[] {
				new ComponentRecord{ComponentCategoryId=moduleCategories[0], Name ="RPIGPIO",Description="Raspberry Pi GPIO",Type=new TypeRecord{ } },
				new ComponentRecord{ComponentCategoryId=moduleCategories[0],Name ="PCA9685",Description="PLA 9685 Controller",Type=new TypeRecord{ } }
			}.Select(core.PluginData.Create).ToArray();

			//core.PluginData.Create(new ComponentRecord { Name = "", Type = new TypeRecord { }, Description = "" });
			//core.PluginData.Create(new ComponentRecord { Name = "", Type = new TypeRecord { }, Description = "" });
			//core.PluginData.Create(new ComponentRecord { Name = "", Type = new TypeRecord { }, Description = "" });
			//core.Hardware.HardwareModuleTypes.Create(new TypeInfo)
			//var modules = new HardwareModule[] {
			//	//new(moduleTypes[0].ModuleTypeId,"GPIO"),
			//	//new(moduleTypes[1].ModuleTypeId,"PCA9685"),
			//}.Select(core..Modules.Create).ToArray();
			//;
		}
	}

	[ConfigProfile("Basic", "Add types for included hardware modules, connectors & channels but does not configure them.")]
	public class BasicProfile : ConfigurationProfile {
		public override void Apply(ILeviathanCore core) {
			var moduleTypes = new Category[] {
				new Category { Name = "Hardware Module", Description = "Hardware components & drivers" }
			}.Select(core.PluginCategoryData.Create).ToArray();
		}
	}

	[ConfigProfile("Hardcore Mode", "Do nothing.  I will awaken the leviathan myself.")]
	public class HardcoreProfile : ConfigurationProfile {
		public override void Apply(ILeviathanCore core) {

		}
	}
}