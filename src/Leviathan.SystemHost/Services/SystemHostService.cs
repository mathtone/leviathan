using Leviathan.Data.Npgsql;
using Leviathan.Services.Sdk;
using Leviathan.SystemHost.Data;
using Mathtone.Sdk.Data;
using Mathtone.Sdk.Data.Npgsql;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Leviathan.SystemHost.Services {

	public class SystemHostService : LeviathanServiceBase<SystemHostServiceConfig>, ISystemHostService, IHostedService {

		private readonly ISystemDataService _systemData;
		private readonly IHost _host;
		private readonly ISystemDbConnectionProvider _systemDb;

		public SystemHostService(ILogger<SystemHostService> log, SystemHostServiceConfig config, ISystemDataService systemData, IHost host, ISystemDbConnectionProvider systemDb) :
			base(log, config) {
			_systemData = systemData;
			_host = host;
			_systemDb = systemDb;
		}

		public async Task StartAsync(CancellationToken cancellationToken) {
			var cfg = await _systemData.GetSystemConfig();
			if (!cfg.IsInitialized) {
				Log.LogInformation("System is not configured. Starting configuration service.");
			}
		}

		public async Task StopAsync(CancellationToken cancellationToken) {
			await Task.CompletedTask;
		}

		public async Task ApplyConfig(SystemConfig config) {
			await _systemData.Configure(config);

			//set pg password
			await using (var cn = await _systemDb.OpenAsync()) {
				await cn
					.TextCommand($"ALTER ROLE postgres WITH PASSWORD '@password'")
					.WithTemplate("@password", config.PostgresSystemPassword!)
					.ExecuteNonQueryAsync();

				await cn
					.TextCommand("select pg_terminate_backend(pid) from pg_stat_activity where datname='leviathan_system';")
					.ExecuteNonQueryAsync();

				await cn
					.TextCommand("DROP DATABASE IF EXISTS leviathan_system;")
					.ExecuteNonQueryAsync();

				await cn
					.TextCommand("CREATE DATABASE leviathan_system;")
					.ExecuteNonQueryAsync();
			}

			//apply config and stop application
			var exec = Environment.ProcessPath!;
			Log.LogInformation("Applying system configuration. Restarting application: {exec}", exec);
			var startinfo = new ProcessStartInfo(exec, "6000");
			Process.Start(startinfo);
			await _host.StopAsync();
		}

		//init DB
	}

	public interface IUserService {
		
	}

	public class UserService : IUserService {

	}	

	public interface ISystemHostService {
		Task ApplyConfig(SystemConfig config);
	}

	public class SystemHostServiceConfig {

	}
}