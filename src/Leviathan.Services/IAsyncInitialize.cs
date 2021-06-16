using System;
using System.Threading.Tasks;

namespace Leviathan.Services {
	public interface IAsyncInitialize {
		Task Initialization { get; }
	}

	public abstract class LeviathanServiceBase : IAsyncInitialize {

		public Task Initialization { get; protected set; }

		public LeviathanServiceBase() {
			//Initialization = InitializeAsync();
		}

		//private Task InitializeAsync() => Task.CompletedTask;
	}
}