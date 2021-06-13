using Leviathan.Core;
using Leviathan.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Initialization {

	public interface IInitializationService {
		void FactoryReset();
		void Configure(SystemConfiguration config);
		void ApplyProfile(string configName);
		IEnumerable<ConfigurationProfileListing> ListProfiles();
		void CompleteInitialization();
		SystemConfiguration CurrentConfig();
	}

	public class InitializationService : IInitializationService {

		protected ILeviathanCore Core { get; }
		protected SystemConfiguration Config { get; set; }

		public InitializationService(ILeviathanCore core) {
			this.Core = core;
			this.Config = core.Config;
		}

		public void FactoryReset() {
			if (Core.SystemDbData.LocateDB(Core.Config.DbName)) {
				Core.SystemDbData.DropDB(Core.Config.DbName);
			}
			Core.SystemDbData.CreateDB(Core.Config.DbName);
		}

		public void ApplyProfile(string configName) {
			var listing = ListProfiles().Single(p => p.Name == configName);
			var profile = (IConfigurationProfile)Activator.CreateInstance(Type.GetType(listing.Type));
			profile.Apply(Core);
			//throw new NotImplementedException();
		}

		static IEnumerable<Assembly> GetLocalAssemblies() =>
			 AppDomain.CurrentDomain.GetAssemblies();

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


		public void CompleteInitialization() {
			throw new NotImplementedException();
		}

		public void Configure(SystemConfiguration config) {
			this.Config = config;
			if (Core.SystemDbData.LocateDB(config.DbName)) {
				;
			}
			else {
				Core.SystemDbData.CreateDB(config.DbName);
			}

			//Create Db;
		}

		public SystemConfiguration CurrentConfig() => this.Config;
	}
}
