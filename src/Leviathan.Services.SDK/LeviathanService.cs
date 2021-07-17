using System;
using System.Threading.Tasks;

namespace Leviathan.Services.SDK {

	public interface ILeviathanService : IAsyncInitialize {

	}

	public class LeviathanService : ILeviathanService {

		public virtual Task Initialize { get; }

		public LeviathanService() {
			Initialize = InitializeAsync();
		}

		Task InitializeAsync() => Task.CompletedTask;

		protected virtual async Task<T> WhenInitialized<T>(Func<T> selector) {
			await Initialize;
			return selector();
		}
	}
}