using Leviathan.Services.SDK;
using Leviathan.System.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Core {
	public interface ITheLeviathan {
		Task Start();
		Task Stop();
	}

	[SingletonService(typeof(ITheLeviathan))]
	public class TheLeviathan : LeviathanService, ITheLeviathan {

		public override Task Initialize { get; }
		ILeviathanSystem System { get; }

		public TheLeviathan(ILeviathanSystem system) {
			System = system;
			Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {
			await base.Initialize;
			await System.Initialize;
		}

		public async Task Start() {
			await Initialize;
			;
		}

		public async Task Stop() {
			await Task.CompletedTask;
		}
	}
}