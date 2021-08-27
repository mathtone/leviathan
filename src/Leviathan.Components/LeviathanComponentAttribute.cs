using System;
using System.Collections.Generic;
using System.Reflection;

namespace Leviathan.Components {

	public abstract class LeviathanComponentAttribute : Attribute {
		public string ComponentTypeDescription { get; }
		public LeviathanComponentAttribute(string componentTypeDescription) {
			ComponentTypeDescription = componentTypeDescription;
		}
	}

	public interface IComponentsService {
		IEnumerable<Assembly> GetAssemblies();
		IEnumerable<Type> GetTypes();
		IEnumerable<ComponentInfo> GetLeviathanComponents<T>() where T : LeviathanComponentAttribute;
	}
}