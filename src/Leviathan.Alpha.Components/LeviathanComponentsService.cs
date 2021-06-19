using Leviathan.Alpha.Configuration;
using Leviathan.Services;
using Leviathan.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {

	public interface ILeviathanComponentsService : IAsyncInitialize {
		IEnumerable<ComponentListing> GetAllComponentTypes();
	}

	public class LeviathanComponentsService : ILeviathanComponentsService {

		public Task Initialize { get; }

		public LeviathanComponentsService(ISystemConfigService<AlphaSystemConfiguration> config) {
			this.Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {
			await Task.CompletedTask;
			;
		}

		public IEnumerable<ComponentListing> GetAllComponentTypes() {

			var currentAssembly = Assembly.GetExecutingAssembly().Location;
			var path = Path.GetDirectoryName(currentAssembly);
			var loaded = new Dictionary<string, Assembly>();

			foreach (var assembly in GetLoadedAssemblies(path)) {
				if (!loaded.TryAdd(assembly.Location, assembly)) {
					loaded[assembly.Location] = assembly;
				}
			}

			foreach (var dll in Directory.GetFiles(path, "*.dll")) {
				if (!loaded.ContainsKey(dll) && dll != currentAssembly) {
					loaded.Add(dll, Assembly.LoadFile(dll));
				}
			}

			foreach (var assembly in loaded.Values) {
				foreach (var type in assembly.DefinedTypes.Where(t => t.IsPublic)) {
					var attr = type.GetCustomAttributes<LeviathanComponentAttribute>();
					if (attr != null) {
						foreach (var a in attr) {
							yield return new ComponentListing {
								Name = a.Name,
								Description = a.Description,
								Category = a.Category,
								TypeName = type.FullName,
								AssemblyName = type.Assembly.FullName,
								AssemblyPath = type.Assembly.Location,
							};
						}
					}
				}
			}
		}

		static IEnumerable<Assembly> GetLoadedAssemblies(string path) => AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(a => !a.IsDynamic && Path.GetDirectoryName(a.Location) == path);
	}

	public class ComponentDescriptor {
		public LeviathanComponentAttribute Attribute { get; set; }
		public Type Type { get; set; }
	}

	public class ComponentListing {
		//public LeviathanComponentAttribute Attribute { get; init; }
		public string Name { get; init; }
		public string Description { get; init; }
		public string Category { get; init; }
		public string TypeName { get; init; }
		public string AssemblyName { get; init; }
		public string AssemblyPath { get; init; }
	}

}
