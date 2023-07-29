using Leviathan.Data.Npgsql;
using Leviathan.Services.Sdk;
using Leviathan.SystemHost.Data;
using Mathtone.Sdk.Data.Npgsql;
using System.Runtime.CompilerServices;

namespace Leviathan.SystemHost.Services {

	public class SystemHostService : LeviathanServiceBase<SystemHostServiceConfig>, IHostedService {
		//private readonly IStartupService _startup;
		private readonly ISystemDataService _systemData;

		public SystemHostService(ILogger<SystemHostService> log, SystemHostServiceConfig config, ISystemDataService systemData) :
			base(log, config) => (_systemData) = (systemData);

		public async Task StartAsync(CancellationToken cancellationToken) {
			var cfg = await _systemData.GetSystemConfig();
			if (!cfg.IsInitialized) {
				Log.LogInformation("System is not configured. Starting configuration service."); 
			}
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}
	}

	//public interface IStartupService {
	//	Task<bool> IsConfigured { get; }
	//}

	//public class StartupService : LeviathanServiceBase, IStartupService {
	//	private readonly ISystemDataService _systemData;

	//	public StartupService(ILogger<StartupService> log, ISystemDataService systemData) :
	//		base(log) {
	//		_systemData = systemData;
	//	}

	//	public Task<bool> IsConfigured =>
	//		Task.FromResult(File.Exists("system/systemsettings.json"));
	//}

	public class SystemHostServiceConfig {

	}
}