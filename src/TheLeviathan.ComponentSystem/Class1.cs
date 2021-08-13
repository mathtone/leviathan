using Leviathan.Components.Sdk;
using Leviathan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TheLeviathan.ComponentSystem {

	public interface IComponentsService {
		IEnumerable<Assembly> GetAssemblies();
		IEnumerable<Type> GetTypes();
		IEnumerable<Type> GetComponents<T>() where T : LeviathanComponentAttribute;
		IEnumerable<Type> GetComponents();
	}

	[SingletonService(typeof(IComponentsService))]
	public class ComponentsService : IComponentsService {
		public ComponentsService() {
			;
		}

		public IEnumerable<Assembly> GetAssemblies() {
			var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			foreach (var f in Directory.GetFiles(location, "*.dll")) {
				yield return GetAssembly(f);
			}
		}

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

		public IEnumerable<Type> GetComponents() =>
			GetComponents<LeviathanComponentAttribute>();

		public IEnumerable<Type> GetComponents<T>() where T : LeviathanComponentAttribute =>
			GetTypes().Where(t => t.CustomAttributes.Any(a => a.AttributeType.IsAssignableTo(typeof(T))));
	}
}