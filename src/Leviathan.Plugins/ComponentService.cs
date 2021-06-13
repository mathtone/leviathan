﻿using Leviathan.DataAccess;
using Leviathan.Hardware;
using Leviathan.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Plugins {

	public class Category {
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public interface IComponentData : IListRepository<ComponentRecord> {

	}
	public interface IComponentCategoryData : IListRepository<Category> { }

	public interface IComponentService {
		IComponentData Plugins { get; }
		IComponentCategoryData Categories { get; }
		ComponentCatalog Catalog();
	}

	public class ComponentCatalog {
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<ComponentRecord> Registered { get; set; }
		public IEnumerable<ComponentDescriptor> Found { get; set; }
	}

	public class ComponentRecord {
		public long? Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public TypeRecord Type { get; set; }
		public bool IsRegistered => Id.HasValue;
	}

	public class ComponentService : IComponentService {

		public IComponentData Plugins { get; }
		public IComponentCategoryData Categories { get; }

		public ComponentService(IComponentData plugins, IComponentCategoryData categories) {
			this.Plugins = plugins;
			this.Categories = categories;
		}

		public ComponentCatalog Catalog() => new() {
			Categories = Categories.List(),
			Registered = Plugins.List(),
			Found = GetAllComponentTypes()
		};

		static IEnumerable<ComponentDescriptor> GetAllComponentTypes() {

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
				foreach (var type in assembly.DefinedTypes) {
					if (type.IsPublic) {
						var attr = type.GetCustomAttribute<LeviathanComponentAttribute>();
						if (attr != null) {
							yield return new ComponentDescriptor {
								Attribute = attr,
								Type = type
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
}