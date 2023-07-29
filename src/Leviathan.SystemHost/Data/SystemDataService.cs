using Leviathan.Services.Sdk;
using Leviathan.SystemHost.Services;

namespace Leviathan.SystemHost.Data {

	public class SystemDataService : LeviathanServiceBase, ISystemDataService {

		private readonly ISystemDbConnectionProvider _systemDb;
		SystemConfig _systemConfig;

		public SystemDataService(ILogger<SystemDataService> log, ISystemDbConnectionProvider systemDb) : base(log) =>
			_systemDb = systemDb;

		public async Task<SystemConfig> GetSystemConfig() {
			return _systemConfig ??= new SystemConfig() { };
		}

		public Task Configure(SystemConfig config) {
			throw new Exception("Not implemented");
		}
	}

	public class SystemConfig {
		public bool IsInitialized { get; set; } = false;
		public string PostgresSystemUserName { get; set; } = "postgres";
		public string PostgresSystemPassword { get; set; } = "test!1234";
	}

	public interface ISystemDataService {
		Task<SystemConfig> GetSystemConfig();
		Task Configure(SystemConfig config);
	}
}