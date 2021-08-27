using Leviathan.Components;
using Leviathan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TheLeviathan.FileSystem;

namespace TheLeviathan.ComponentSystem {
	

	[TransientService(typeof(IComponentsService))]
	public class ComponentsService : IComponentsService {

		IFileSystemService _fileSystem;
		
		public ComponentsService(IFileSystemService fileSystem) =>
			(_fileSystem) = (fileSystem);

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


	
}
