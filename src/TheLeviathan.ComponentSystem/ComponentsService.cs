using Leviathan.Components.Sdk;
using Leviathan.Services;
using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
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
		//IEnumerable<Type> _types;
		//IEnumerable<ComponentInfo> _components;

		public ComponentsService(IFileSystemService fileSystem) => _fileSystem = fileSystem;

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

	public class ComponentListing {
		public int Id { get; init; }
		public string TypeName { get; init; }
		public string AssemblyName { get; init; }
		public string[] ComponentTypes { get; init; }
	}

	[ApiComponent("system")]
	public class ComponentsController : ControllerBase {

		IComponentsService _service;
		public ComponentsController(IComponentsService service) {
			_service = service;
		}
		[HttpGet]
		public IEnumerable<ComponentListing> List() => _service.GetLeviathanComponents<LeviathanComponentAttribute>()
			.Select(c => new ComponentListing {
				Id = c.Id,
				AssemblyName = c.Type.Assembly.FullName,
				TypeName = c.Type.Name,
				ComponentTypes = c.ComponentAttributes.Select(a => a.ComponentTypeDescription).ToArray()
			});
	}
}