using Leviathan.Common;
using System;
using System.Collections.Generic;

namespace Leviathan.Components.Sdk {
	public abstract class LeviathanComponentAttribute : Attribute {
		public string ComponentTypeDescription { get; }
		public LeviathanComponentAttribute(string componentTypeDescription) {
			ComponentTypeDescription = componentTypeDescription;
		}
	}

	public interface IComponentData {
		IAsyncRepository<int, AssemblyRecord> Assembly { get; }
	}

	public interface IAssemblyData : IAsyncRepository<int, AssemblyRecord> {
		IEnumerable<AssemblyRecord> List();
	}

	public record AssemblyRecord {
		public int Id { get; init; }
		public string Path { get; init; }
		public string Description { get; set; }
	}

	public record TypeRecord {
		public int Id { get; init; }
		public int AssemblyId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public record ComponentRecord {
		public int Id { get; init; }
		public int TypeId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}


	public record CategoryRecord {
		public int Id { get; init; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public record ComponentCategoryRecord : CategoryRecord { }

	
}