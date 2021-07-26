using Leviathan.Components.SDK;
using Leviathan.Services.SDK;
using Leviathan.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {
	public interface IComponentsService : ILeviathanService {

		Task<ComponentsServiceCatalog> Catalog();
		Task<IEnumerable<ComponentInfo>> AvailableComponents();
	}

	[SingletonService(typeof(IComponentsService))]
	public class ComponentsService : LeviathanService, IComponentsService {

		ILogger<ComponentsService> _log;
		IServiceProvider _services;

		public override Task Initialize { get; }

		public ComponentsService(ILogger<ComponentsService> log, IServiceProvider services) {
			(_log, _services) = (log, services);
			Initialize = InitializeAsync();
		}

		protected async Task InitializeAsync() {
			await base.Initialize;
			_log.LogInformation($"{typeof(ComponentsService)} Initialized");
		}

		public async Task<ComponentsServiceCatalog> Catalog() => new() {
			Components = (await AvailableComponents()).Select(c => new ComponentListing {
				Id = c.Id,
				Name = c.Name,
				AttributeTypeNames = c.AttributeTypes.Select(t => t.Name).ToArray(),
				TypeName = c.Type.Name
			})
		};

		public Task<IEnumerable<ComponentInfo>> AvailableComponents() => WhenInitialized(GetAllAvailableComponents);

		protected static IEnumerable<ComponentInfo> GetAllAvailableComponents() {

			var localPath = AppDomain.CurrentDomain.BaseDirectory;

			foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
				if (!a.IsDynamic && a.Location.StartsWith(localPath)) {

					var types = a.GetExportedTypes();

					foreach (var t in types) {

						var attr = t.GetCustomAttributes<LeviathanComponentAttribute>();

						if (attr.Any()) {
							yield return new ComponentInfo {
								Name = t.Name,
								Type = t,
								AttributeTypes = attr.Select(a => a.GetType()).ToArray()
							};
						}
					}
				}
			}
		}
	}
}