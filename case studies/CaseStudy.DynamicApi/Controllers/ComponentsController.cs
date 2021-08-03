using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CaseStudy.DynamicApi.Controllers {
	public class ComponentsConfig {
		
		public IEnumerable<ComponentListing> Components { get; set; }
	}

	public class ComponentsCatalog {
		public IEnumerable<AssemblyListing> Assemblies { get; set; }
		public IList<ModuleListing> Modules { get; set; }
	}

	[ApiController, Route("api/[controller]")]
	public class ComponentsController : ControllerBase {
		public ComponentsController() {
		}

		[HttpGet]
		public async Task<ComponentsCatalog> Catalog() {
			return new() {
				Assemblies = ListAssemblies().ToArray(),
				Modules = await ListModules().ToArrayAsync()
			};
		}

		static IEnumerable<AssemblyListing> ListAssemblies() =>
			Directory.EnumerateFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")
				.Select(path=>new AssemblyListing {
					Name = AssemblyName.GetAssemblyName(path).FullName,
					Path = path
				});
			
		//	{
		//	var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		//	foreach (var file in Directory.EnumerateFiles(directory, "*.dll")) {
		//		;
		//	}
		//	yield return null;
		//}

		static async IAsyncEnumerable<ModuleListing> ListModules() {

			yield return null;
		}
	}

	public interface IListing<ID> {
		ID Id { get; }
		string Name { get; }
	}

	public class Listing : Listing<int> {}

	public class Listing<ID> {
		public ID Id { get; init; }
		public string Name { get; set; }
		public string Description{ get; set; }
	}

	public class ModuleListing : Listing<int>{
		public IList<AssemblyListing> Assemblies { get; set; }
	}

	public class AssemblyListing : Listing<int> {
		public string Path { get; set; }
	}

	public class ComponentListing : Listing<int> {
		public string AssemblyName { get; set; }
		public string TypeName { get; set; }
		public bool IsActive { get; set; }
	}
}