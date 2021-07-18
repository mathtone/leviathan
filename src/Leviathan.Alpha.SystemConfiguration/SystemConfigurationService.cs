using Leviathan.Alpha.Components;
using Leviathan.Alpha.Logging;
using Leviathan.Services.SDK;
using Leviathan.SystemConfiguration.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Leviathan.Alpha.SystemConfiguration {
	public interface ISystemConfigurationService : ILeviathanService {
		Task<SystemConfigurationServiceCatalog> Catalog();
		Task ApplyProfile(string name);
	}

	[SingletonService(typeof(ISystemConfigurationService))]
	public class SystemConfigurationService : LeviathanService, ISystemConfigurationService {

		ILoggingService _log;
		IServiceProvider _services;
		IComponentsService _components;
		IDictionary<string, Type> _profiles;

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
			this._profiles = (await _components.AvailableComponents())
				.Where(c => c.AttributeType == typeof(SystemProfileAttribute))
				.Select(p => p.Type)
				.ToDictionary(t => t.Name);
		}

		public async Task<SystemConfigurationServiceCatalog> Catalog() {
			await Initialize;
			return new SystemConfigurationServiceCatalog {
				Profiles = _profiles.Values
					.Select(p => new ProfileListing {
						Name = p.Name,
						Description = $"Profile: {p.Name}"
					})
			};
		}

		public async Task ApplyProfile(string name) {
			await Initialize;
			foreach (var profile in GetRequiredProfiles(_profiles[name])) {
				await _services.CreateInstance<ISystemProfile>(profile).Apply();
			}
		}

		protected static IEnumerable<Type> GetRequiredProfiles(Type profileType) {
			var rtn = new HashSet<Type>();
			foreach (var attr in profileType.GetCustomAttributes<RequireProfileAttribute>(true)) {
				foreach (var t in GetRequiredProfiles(attr.Type)) {
					if (!rtn.Contains(t)) {
						rtn.Add(t);
						yield return t;
					}
				}
			}
			if (!rtn.Contains(profileType)) {
				rtn.Add(profileType);
				yield return profileType;
			}
		}
	}

	public class SystemConfiguration {

	}

	public static class IServiceProviderExtensions {
		public static T CreateInstance<T>(this IServiceProvider services, Type type) {

			var constructors = type.GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			foreach (var c in constructors.OrderByDescending(c => c.GetParameters().Length)) {
				var paramTypes = c.GetParameters().Select(p => p.ParameterType);
				var svcParams = paramTypes.Select(t => services.GetService(t));
				return (T)Activator.CreateInstance(type, svcParams.ToArray());
			}
			throw new Exception("Could not construct target");
		}
	}
}