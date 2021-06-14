using Leviathan.Core;
using Leviathan.DataAccess;
using Leviathan.SDK;
using Leviathan.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Initialization {



	//public class InitializationService  {

	//	protected ILeviathanCore Core { get; }
	//	protected ISystemDbData SystemData => Core.SystemData;// { get; }
	//	protected ISystemConfiguration Config => Core.Config;//{ get; set; }
		


	//	public InitializationService(ILeviathanCore core) {
	//		this.Core = core;
	//		//this.SystemData = systemData;
	//		//this.Config = config;
	//	}

	//	//public void FactoryReset() {
	//	//	if (SystemData.LocateDB(Config.InstanceName)) {
	//	//		SystemData.DropDB(Config.InstanceName);
	//	//	}
	//	//	(_ = ResetApplied)?.Invoke(this, new InitializationEventArgs());
	//	//}

	//	public void ApplyProfile(string configName) {
	//		var listing = ListProfiles().Single(p => p.Name == configName);
	//		var profile = (IConfigurationProfile)Activator.CreateInstance(Type.GetType(listing.Type));

	//		//profile.Apply()
	//		//throw new NotImplementedException();
	//	}

	//	static IEnumerable<Assembly> GetLocalAssemblies() =>
	//		 AppDomain.CurrentDomain.GetAssemblies();

	//	public IEnumerable<ConfigurationProfileListing> ListProfiles() =>
	//		GetLocalAssemblies()
	//			.SelectMany(a => a.DefinedTypes)
	//			.Select(t => new {
	//				Description = t.GetCustomAttribute<ConfigProfileAttribute>(),
	//				Type = t
	//			})
	//			.Where(r => r.Description != null)
	//			.Select(r => new ConfigurationProfileListing {
	//				Name = r.Description.ProfileName,
	//				Description = r.Description.Description,
	//				Type = r.Type.FullName
	//			});

	//	public void CompleteInitialization() {
	//		throw new NotImplementedException();
	//	}

	//	public void Configure(SystemConfiguration config) {

	//		//Core.Config = config;
	//		SystemData.CreateDB(Config.InstanceName);
	//		//(_ = ConfigurationApplied)?.Invoke(this, new ConfigurationAppliedArgs(Config));

	//		//if (Core.SystemDbData.LocateDB(config.DbName)) {
	//		//	;
	//		//}
	//		//else {
	//		//	Core.SystemDbData.CreateDB(config.DbName);
	//		//}

	//		//Create Db;
	//	}

	//	public ISystemConfiguration CurrentConfig() => this.Config;
	//}
}