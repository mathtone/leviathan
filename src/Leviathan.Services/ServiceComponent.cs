using System.Threading.Tasks;

namespace Leviathan.Services {
	public interface IAsyncInitialize {
		Task Initialize { get; }
	}

	public interface IServiceComponent {

	}

	public abstract class ServiceComponent : IServiceComponent, IAsyncInitialize {
		public Task Initialize { get; protected set; }
		protected virtual async Task InitializeAsync() => await Task.CompletedTask;
	}
}