using Leviathan.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.Services {

	public interface IServiceComponent {}
	public abstract class ServiceComponent : IServiceComponent, IAsyncInitialize {
		public Task Initialize { get; protected set; } = Task.CompletedTask;
		protected virtual async Task InitializeAsync() => await Task.CompletedTask;
		
		public ServiceComponent() {

		}

		protected async Task WhenInitialized(Action action) {
			await Initialize;
			action();
		}

		protected async Task<T> WhenInitialized<T>(Func<T> function) {
			await Initialize;
			return function();
		}
	}
}