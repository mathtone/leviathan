using Leviathan.Alpha.Components;
using Leviathan.Alpha.Components.Profiles;
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

namespace Leviathan.Alpha.Startup {
	public interface IStartupService {
		Task<StartupCatalog> CatalogAsync();
		Task<IEnumerable<ComponentListing>> SystemProfiles();
		Task ActivateProfile(string profileTypeName);
		//void SetAdminCredentials(CredentialsConfig credentials);
	}

	public class StartupCatalog {

		public IHostEnvironment Environment { get; init; }
	}

	public class StartupService : ServiceComponent, IStartupService {

		ILeviathanSystem System { get; }
		IComponentsService Components { get; }
		//IServiceProvider Services { get; }

		protected Dictionary<string, ComponentInfo> Profiles { get; set; }
		public StartupService(ILeviathanSystem system, IComponentsService components) {
			this.System = system;
			this.Components = components;
			this.Initialize = InitializeAsync();
			//this.Services = services;
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
			var profileInfo = Profiles[profileTypeName];
			var profile = Components.Activate<ISystemProfileComponent>(profileInfo.SystemType);
			await profile.Apply();
			//var profile = (ISystemProfileComponent)Activator.CreateInstance(p.SystemType);
			//var svc = Services.GetService(p.SystemType);
			;
		}
	}
}