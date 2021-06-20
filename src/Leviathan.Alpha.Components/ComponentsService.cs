﻿using Leviathan.Components;
using Leviathan.SDK;
using Leviathan.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {
	public interface IComponentsService {
		IReadOnlyDictionary<string,ComponentInfo> Components { get;  }
		Task<ComponentsCatalog> CatalogAsync();
		T Activate<T>(Type type);
	}

	public class PluginListing {
		public string Name { get; init; }
		public string AssemblyName { get; init; }
		public string Location { get; set; }
	}

	public class ComponentListing {
		public string Name { get; init; }
		public string Description { get; init; }
		public ComponentCategory Category { get; init; }
		public string TypeName { get; init; }
		public string AssemblyName { get; set; }
	}

	public class ComponentsCatalog {
		public IEnumerable<PluginListing> Plugins { get; init; }
		public IEnumerable<ComponentListing> Components { get; init; }
	}


	public class ComponentsService : ServiceComponent, IComponentsService {

		ILeviathanSystem System { get; }
		IServiceProvider Services { get; }

		Dictionary<string, Assembly> plugins;
		public IReadOnlyDictionary<string, ComponentInfo> Components { get; protected set; }

		public ComponentsService(ILeviathanSystem system,IServiceProvider services) {
			this.System = system;
			this.Services = services;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			plugins = GetPluginAssemblies().ToDictionary(p => p.Location);
			Components = plugins.Values.SelectMany(ListComponents).ToDictionary(a => a.Name);
		}

		public async Task<ComponentsCatalog> CatalogAsync() {
			await Initialize;
			return new ComponentsCatalog {
				Plugins = plugins.Values.Select(a => new PluginListing {
					Name = Path.GetFileNameWithoutExtension(a.Location),
					AssemblyName = a.FullName,
					Location = a.Location
				}),
				Components = Components.Values.Select(c => new ComponentListing {
					Name = c.Name,
					Category = c.Category,
					TypeName = c.SystemType.Name,
					AssemblyName = Path.GetFileName(c.SystemType.Assembly.Location)
				})
			};
		}

		

		static IEnumerable<Assembly> GetPluginAssemblies() {

			var currentAssembly = Assembly.GetExecutingAssembly().Location;
			var localPath = Path.GetDirectoryName(currentAssembly);
			var loaded = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(a => !a.IsDynamic && Path.GetDirectoryName(a.Location) == localPath)
				.ToDictionary(a => a.Location);

			foreach (var dll in Directory.GetFiles(localPath, "*.dll")) {
				if (!loaded.ContainsKey(dll) && dll != currentAssembly) {
					try {
						loaded.Add(dll, Assembly.LoadFile(dll));
					}
					catch {
						;
					}
				}
			}
			foreach (var assembly in loaded.Values.Where(IsPluginAssembly)) {
				yield return assembly;
			}
		}

		static bool IsPluginAssembly(Assembly assembly) =>
			assembly.GetCustomAttribute<LeviathanPluginAttribute>() != default;

		static IEnumerable<ComponentInfo> ListComponents(Assembly assembly) {
			foreach (var t in assembly.DefinedTypes.Where(t => t.IsPublic)) {
				foreach (var attr in t.GetCustomAttributes<LeviathanComponentAttribute>()) {
					yield return new ComponentInfo {
						Name = attr.Name,
						Description = attr.Description,
						Category = attr.Category,
						SystemType = t
					};
				}
			}
		}

		public T Activate<T>(Type type) {
			try {
				var services = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
					.SingleOrDefault()
					.GetParameters()
					.Select(p => Services.GetRequiredService(p.ParameterType)).ToArray();

				return (T)Activator.CreateInstance(type, services);
			}
			catch {
				throw new Exception("Cannot resolve constructor for this system profile.");
			}
		}
	}
}