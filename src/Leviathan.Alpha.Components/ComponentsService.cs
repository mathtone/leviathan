using Leviathan.Alpha.Logging;
using Leviathan.Components.SDK;
using Leviathan.Services.SDK;
using Leviathan.Utilities;
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
		ILoggingService _log;
		public override Task Initialize { get; }

		public ComponentsService(ILoggingService logging) {
			this._log = logging;
			Initialize = InitializeAsync();
		}

		protected async Task InitializeAsync() {
			await base.Initialize;
		}

		public async Task<ComponentsServiceCatalog> Catalog() => new() {
			Components = (await AvailableComponents()).Select(c => new ComponentListing {
				Id = c.Id,
				Name = c.Name,
				AttributeTypeName = c.AttributeType.Name,
				TypeName = c.Type.Name
			})
		};

		public Task<IEnumerable<ComponentInfo>> AvailableComponents() =>
			WhenInitialized(GetAllAvailableComponents);

		protected static IEnumerable<ComponentInfo> GetAllAvailableComponents() {

			var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var dlls = Directory.GetFiles(localPath, "*.dll");
			var types = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(a => !a.IsDynamic && Path.GetDirectoryName(a.Location) == localPath)
				.SelectMany(a => a.GetExportedTypes());

			foreach (var t in types) {
				var attr = t.GetCustomAttribute<LeviathanComponentAttribute>();
				if (attr != null) {
					yield return new ComponentInfo {
						Name = t.Name,
						Type = t,
						AttributeType = attr.GetType()
					};
				}
			}
		}
	}
}