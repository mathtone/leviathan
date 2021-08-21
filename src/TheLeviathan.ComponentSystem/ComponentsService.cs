using Leviathan.Components.Sdk;
using Leviathan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TheLeviathan.FileDataSystem;

namespace TheLeviathan.ComponentSystem {

	public interface IComponentsService {
		IEnumerable<Assembly> GetAssemblies();
		IEnumerable<Type> GetTypes();
		IEnumerable<ComponentInfo> GetLeviathanComponents<T>() where T : LeviathanComponentAttribute;
	}

	[SingletonService(typeof(IComponentsService))]
	public class ComponentsService : IComponentsService {

		IFileSystemService _fileSystem;
		IAssemblyData _data;

		public ComponentsService(IFileSystemService fileSystem, IAssemblyData data) =>
			(_fileSystem, _data) = (fileSystem, data);

		public IEnumerable<Assembly> GetAssemblies() =>
			_fileSystem.LocalFiles("*.dll").Select(GetAssembly);

		static Assembly GetAssembly(string fileName) {
			try {
				return Assembly.Load(AssemblyName.GetAssemblyName(fileName));
			}
			catch (FileNotFoundException ex) {
				return Assembly.LoadFile(fileName);
			}
		}

		public IEnumerable<Type> GetTypes() =>
			GetAssemblies().SelectMany(a => a.GetExportedTypes());


		public IEnumerable<ComponentInfo> GetLeviathanComponents<T>() where T : LeviathanComponentAttribute {
			foreach (var t in GetTypes()) {
				var attr = t.GetCustomAttributes<T>().ToArray();
				if (attr.Any()) {
					yield return new ComponentInfo {
						Id = 0,
						Type = t,
						ComponentAttributes = attr
					};
				}
			}
		}
	}

	public class ComponentInfo {
		public int Id { get; init; }
		public Type Type { get; set; }
		public LeviathanComponentAttribute[] ComponentAttributes { get; set; }
	}
}