using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Npgsql;
using Leviathan.System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Services {
	public class StartupService : IStartupService {

		ISystemConfigService<AlphaSystemConfiguration> ConfigService { get; }
		IDataSystemService DataSystem { get; }
		AlphaSystemConfiguration CurrentConfig { get; set; }
		StartupServiceStatus Status { get; set; }
		

		public StartupService(ISystemConfigService<AlphaSystemConfiguration> configService, IDataSystemService dataSystem) {
			this.ConfigService = configService;
			this.DataSystem = dataSystem;
			this.CurrentConfig = ConfigService.Config;
		}

		public StartupServiceCatalog Catalog() => new() {
			Status = Status,
			CurrentConfig = CurrentConfig with
			{
				StartupInfo = View(CurrentConfig.StartupInfo)
			}
		};

		static StartupInfo View(StartupInfo info) => info == null ? null : info with
		{
			AdminCredentials = info.AdminCredentials with { Password = "********" },
			DatabaseInfo = info.DatabaseInfo with
			{
				DbServerCredentials = info.DatabaseInfo.DbServerCredentials with { Password = "********" }
			}
		};

		public void SetStartupInfo(StartupInfo startupInfo) {
			this.CurrentConfig = this.CurrentConfig with { StartupInfo = startupInfo };
		}

		public async Task ApplyStartupConfig() {
			this.Status = StartupServiceStatus.ApplyPending;

			await this.ConfigService.SaveAsync(this.CurrentConfig);
			await DataSystem.ReInitialize();

		}
	}

	public enum StartupServiceStatus {
		FirstTime, ApplyPending, StartupComplete
	}

	public record StartupServiceCatalog {
		public StartupServiceStatus Status { get; init; }
		public AlphaSystemConfiguration CurrentConfig { get; init; }
	}

	public interface IStartupService {
		StartupServiceCatalog Catalog();
		void SetStartupInfo(StartupInfo startupInfo);
		Task ApplyStartupConfig();
	}

}