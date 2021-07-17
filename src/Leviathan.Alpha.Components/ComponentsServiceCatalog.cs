using System;
using System.Collections.Generic;
using System.Reflection;

namespace Leviathan.Alpha.Components {

	public class ComponentsServiceCatalog {
		public IEnumerable<ComponentListing> Components { get; init; }
	}

	public class ComponentListing {
		public long Id { get; init; }
		public string Name { get; init; }
		public string TypeName { get; init; }
		public string AttributeTypeName { get; set; }
	}

	public class ComponentInfo {
		public long Id { get; init; }
		public string Name { get; init; }
		public Type Type { get; init; }
		public Type AttributeType { get; set; }
	}
}