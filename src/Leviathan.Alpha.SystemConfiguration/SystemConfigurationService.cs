using Leviathan.Alpha.Components;
using Leviathan.Alpha.Logging;
using Leviathan.Services.SDK;
using Leviathan.SystemConfiguration.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.SystemConfiguration {
	public interface ISystemConfigurationService : ILeviathanService {
		Task<SystemConfigurationServiceCatalog> Catalog();
	}

	public class SystemConfigurationServiceCatalog {
		public IEnumerable<ProfileListing> Profiles { get; init; }
	}

	public class ProfileListing {
		public int Id { get; init; }
		public string Name { get; init; }
		public string Description { get; init; }
	}

	[SingletonService(typeof(ISystemConfigurationService))]
	public class SystemConfigurationService : LeviathanService, ISystemConfigurationService {

		ILoggingService _log;
		IServiceProvider _services;
		IComponentsService _components;

		public override Task Initialize { get; }

		public SystemConfigurationService(ILoggingService log, IComponentsService components, IServiceProvider services) {
			this._log = log;
			this._components = components;
			this._services = services;

			Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {

			await base.Initialize;
			await _components.Initialize;

			//var rt = (await _components.AvailableComponents())
			//	.Where(c => c.AttributeType == typeof(SystemProfileAttribute))
			//	.Select(p => new {
			//		Component = p.Type,
			//		Description = $"Profile: {p.Name}"
			//	})
			//	.ToArray()[1];

			//var pars = rt.Component.GetConstructors()[0].GetParameters().Select(p => _services.GetService(p.ParameterType)).ToArray();
			//var profile = (ISystemProfile)Activator.CreateInstance(rt.Component, pars);
			//profile.Apply();
			//;
		}

		public async Task<SystemConfigurationServiceCatalog> Catalog() {
			await Initialize;
			var components = await _components.AvailableComponents();
			return new SystemConfigurationServiceCatalog {
				Profiles = components
					.Where(c => c.AttributeType == typeof(SystemProfileAttribute))
					.Select(p => new ProfileListing {
						Name = p.Name,
						Description = $"Profile: {p.Name}"
					})
			};
		}
	}
}