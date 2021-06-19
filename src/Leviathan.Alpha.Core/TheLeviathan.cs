using Leviathan.Services;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Core {
	public interface IAmLeviathan : IAsyncInitialize {
		Task Start();
		Task Stop();
	}

	public class TheLeviathan : ServiceComponent, IAmLeviathan {

		protected ILeviathanSystem System { get; }

		public TheLeviathan(ILeviathanSystem system) : base() {
			this.System = system;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
			;
		}
		public async Task Start() {
			await Initialize;
			;
		}

		public async Task Stop() {
			await Initialize;
			;
		}
	}
}