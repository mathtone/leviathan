using Leviathan.Services.Sdk;
using Leviathan.SystemHost.Services;
using System.Text.Json;
using static Leviathan.SystemHost.Data.SystemDataService;

namespace Leviathan.SystemHost.Data {

	public class SystemDataService : LeviathanServiceBase, ISystemDataService {

		private readonly ISystemDbConnectionProvider _systemDb;
		SystemConfig? _systemConfig;

		public SystemDataService(ILogger<SystemDataService> log, ISystemDbConnectionProvider systemDb) : base(log) =>
			_systemDb = systemDb;

		public async Task<SystemConfig> GetSystemConfig() => _systemConfig ??=
			File.Exists("System/SystemConfig.json")?
				JsonSerializer.Deserialize<SystemConfig>(await File.ReadAllTextAsync("System/SystemConfig.json"))! :
				new SystemConfig();

		public async Task Configure(SystemConfig config) {
			if (!Directory.Exists("System"))
				Directory.CreateDirectory("System");

			await File.WriteAllTextAsync("System/SystemConfig.json", JsonSerializer.Serialize(config));
			_systemConfig = null;
		}
	}
	public class SystemConfig {
		public bool IsInitialized { get; set; } = false;
		public string PostgresSystemUserName { get; set; } = "postgres";
		public string PostgresSystemPassword { get; set; } = "test!1234";

		public string AdminLogin { get; set; } = "admin";
		public string AdminPassword{ get; set; } = "admin";
	}

	public interface ISystemDataService {
		Task<SystemConfig> GetSystemConfig();
		Task Configure(SystemConfig config);
	}
}