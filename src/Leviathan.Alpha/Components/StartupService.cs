using Leviathan.Services;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha {

	public interface IStartupService : IAsyncInitialize {
	}

	public class StartupService : LeviathanServiceBase, IStartupService {

		public StartupService() {
			Initialization = InitializeAsync();
		}

		private Task InitializeAsync() =>
			Task.CompletedTask;
	}
}