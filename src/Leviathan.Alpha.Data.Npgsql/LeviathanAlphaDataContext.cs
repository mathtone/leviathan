using Leviathan.SDK;

namespace Leviathan.Alpha.Data.Npgsql {
	public interface ILeviathanAlphaDataContext {
		IListRepository<long, ComponentAssemblyRecord> ComponentAssembly { get; }
		IListRepository<long, ComponentCategoryRecord> ComponentCategory { get; }
		IListRepository<long, ComponentTypeRecord> ComponentType { get; }
	}

	public class LeviathanAlphaDataContext : ILeviathanAlphaDataContext {

		public IListRepository<long, ComponentAssemblyRecord> ComponentAssembly { get; init; }
		public IListRepository<long, ComponentCategoryRecord> ComponentCategory { get; init; }
		public IListRepository<long, ComponentTypeRecord> ComponentType { get; init; }

		public LeviathanAlphaDataContext() {

		}
	}
}