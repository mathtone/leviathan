using Leviathan.Services.SDK;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components {
	public interface IComponentsService : ILeviathanService {

		Task<ComponentsServiceCatalog> Catalog();
	}

	[SingletonService(typeof(IComponentsService))]
	public class ComponentsService : LeviathanService, IComponentsService {

		public override Task Initialize { get; }

		public ComponentsService() {
			Initialize = InitializeAsync();
		}

		protected async Task InitializeAsync() {
			await base.Initialize;

		}

		public async Task<ComponentsServiceCatalog> Catalog() {
			await Initialize;
			return new() {

			};
		}
	}
}