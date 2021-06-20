using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan.Components {

	public interface IComponentsService {
		IReadOnlyDictionary<string, ComponentInfo> Components { get; }
		Task<ComponentsCatalog> CatalogAsync();
		T Activate<T>(Type type);
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
	public class PluginListing {
		public string Name { get; init; }
		public string AssemblyName { get; init; }
		public string Location { get; set; }
	}
}