using System;
using System.Threading.Tasks;
using Leviathan.Services.SDK;
using Leviathan.System.SDK;

namespace Leviathan.Alpha.System {

	[SingletonService(typeof(ILeviathanSystem))]
	public class SystemService : LeviathanService, ILeviathanSystem {

		public override Task Initialize { get; }

		public SystemService() {
			Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {
			await base.Initialize;
		}

		public async Task<SystemServiceCatalog> Catalog() {
			await Initialize;
			return new SystemServiceCatalog {

			};
		}
	}
}