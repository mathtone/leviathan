using System;
using System.Threading.Tasks;
using Leviathan.Alpha.Components;
using Leviathan.Services.SDK;
using Leviathan.System.SDK;

namespace Leviathan.Alpha.System {

	[SingletonService(typeof(ILeviathanSystem))]
	public class SystemService : LeviathanService, ILeviathanSystem {

		protected IComponentsService Components { get; }

		public override Task Initialize { get; }

		public SystemService(IComponentsService components) {
			Components = components;
			Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {
			await base.Initialize;
			await Components.Initialize;

			var components = await Components.Catalog();

			;
		}

		public async Task<SystemServiceCatalog> Catalog() {
			await Initialize;
			return new SystemServiceCatalog {

			};
		}
	}
}