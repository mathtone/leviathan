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
		Task ApplyProfile(string name, IEnumerable<ProfileApplication> applications);
		Task<IEnumerable<ProfileApplication>> GetApplication(string name);
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

		public async Task ApplyProfile(string name, IEnumerable<ProfileApplication> applications) {
			await Initialize;
			foreach (var profile in GetRequiredProfiles(_profiles[name])) {

				var instance = _services.CreateInstance<ISystemProfile>(profile);
				var t = instance.GetType();
				var application = applications.Single(a => a.ProfileName == profile.Name);

				foreach(var p in t.GetProperties()) {
					var attr = p.GetCustomAttribute<ProfilePropertyAttribute>();
					if(attr != null) {
						p.SetValue(instance, application.ApplicationFields.Single(f => f.Name == attr.Name).Value);
						;
					}
				}
				await instance.Apply();
			}
		}

		public Task<IEnumerable<ProfileApplication>> GetApplication(string name) =>
			WhenInitialized(() =>
				GetRequiredProfiles(_profiles[name]).Select(GetApplication)
			);


		protected static ProfileApplication GetApplication(Type type) {
			return new ProfileApplication {
				ProfileName = type.Name,
				ApplicationFields = GetApplicationFields(type)
			};
		}

		protected static IEnumerable<ApplicationField> GetApplicationFields(Type type) {
			foreach (var t in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)) {
				var attr = t.GetCustomAttribute<ProfilePropertyAttribute>();
				if (attr != null) {
					yield return new() {
						Name = attr.Name,
						Description = attr.Description,
						Value = attr.DefaultValue
					};
				}
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