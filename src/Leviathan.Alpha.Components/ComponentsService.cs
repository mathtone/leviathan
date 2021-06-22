using Leviathan.Alpha.Data.Npgsql;
using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.DbDataAccess;
using Leviathan.DbDataAccess.Npgsql;
using Leviathan.SDK;
using Leviathan.Services;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {

	public class ComponentsService : ServiceComponent, IComponentsService {

		ILeviathanAlphaDataContext<NpgsqlConnection> _context;

		//ILeviathanSystem System { get; }
		IServiceProvider Services { get; }
		ILeviathanAlphaDataContextProvider Provider { get; }
		ILeviathanAlphaDataContext<NpgsqlConnection> Context => _context ??= Provider.CreateContext<NpgsqlConnection>();
		NpgsqlConnection Connection => Context.Connection;
		NpgsqlConnection Connect() {
			if(Connection.State != ConnectionState.Open) {
				Connection.Open();
			}
			return Connection;
		}
		Dictionary<string, Assembly> plugins;
		public IReadOnlyDictionary<string, ComponentInfo> Components { get; protected set; }

		public ComponentsService(IServiceProvider services, ILeviathanAlphaDataContextProvider provider) {
			//this.System = system;
			this.Services = services;
			this.Provider = provider;
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

		public long RegisterComponent(Type type) {
			
			var cmd = Connect().CreateCommand(@"SELECT * FROM sys.component_assembly where assembly_path = @path");
			var id = cmd.WithInput("@path", type.Assembly.Location)
				.ExecuteReader()
				.Consume(r => r.Field<long>("id"))
				.SingleOrDefault();

			if (id == 0) {
				var a1 = type.Assembly.GetCustomAttribute<LeviathanPluginAttribute>();
				id = Context.ComponentAssembly.Create(new ComponentAssemblyRecord {
					Name = a1.Name,
					AssemblyName = type.Assembly.FullName,
					AssemblyPath = type.Assembly.Location,
					Description = "Plugin assembly"
				});
			}

			var a2 = type.GetCustomAttribute(typeof(LeviathanComponentAttribute)) as LeviathanComponentAttribute;
			var componentid = Context.ComponentType.Create(new ComponentTypeRecord {
				AssemblyId = id,
				CategoryId = (long)a2.Category,
				TypeName = type.FullName,
				Name = a2.Name,
				Description = a2.Description
			});

			return componentid;
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