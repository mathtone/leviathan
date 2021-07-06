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
	}
}