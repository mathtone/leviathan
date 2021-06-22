using Leviathan.Alpha.Components;
using Leviathan.Components;
using Leviathan.SDK;
using Leviathan.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Leviathan.Alpha.Database;
using Npgsql;

namespace Leviathan.Alpha.Startup {
	

	public class StartupService : ServiceComponent, IStartupService {

		ILeviathanSystem System { get; }
		IComponentsService Components { get; }
		//IDataSystemService<NpgsqlConnection> DataSystem { get; }

		protected Dictionary<string, ComponentInfo> Profiles { get; set; }

		public StartupService(ILeviathanSystem system, IComponentsService components) {
			this.System = system;
			this.Components = components;
			this.Initialize = InitializeAsync();
			//this.DataSystem = dataSystem;
		}

		protected async override Task InitializeAsync() {
			await base.InitializeAsync();
			Profiles = Components.Components.Values.Where(p => p.Category == ComponentCategory.SystemProfile).ToDictionary(p => p.SystemType.FullName);
		}

		public async Task<StartupCatalog> CatalogAsync() {
			await Initialize;
			return new StartupCatalog {
				Environment = System.HostEnvironment.Environment
			};
		}

		public async Task<IEnumerable<ComponentListing>> SystemProfiles() {
			await Initialize;
			return Profiles.Values.Select(p => new ComponentListing {
				Name = p.Name,
				Description = p.Description,
				Category = p.Category,
				TypeName = p.SystemType.FullName,
				AssemblyName = p.SystemType.Assembly.FullName
			});
		}

		public async Task ActivateProfile(string profileTypeName) {
			await Initialize;
			await Components.Activate<ISystemProfileComponent>(Profiles[profileTypeName].SystemType).Apply();
		}

		//public async Task FactoryReset() {
		//	using var cn = DataSystem.ConnectSystem();
		//	await cn.OpenAsync();

		//	;
		//}
	}
}