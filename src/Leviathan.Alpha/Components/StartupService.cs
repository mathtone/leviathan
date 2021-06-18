using Leviathan.Alpha.Configuration;
using Leviathan.Alpha.Npgsql;
using Leviathan.System;
using System.Threading.Tasks;

namespace Leviathan.Alpha {
	public class StartupService : IStartupService {

		AlphaSystemConfiguration CurrentConfig { get; set; }
		StartupServiceStatus Status { get; set; }
		ISystemConfigService<AlphaSystemConfiguration> ConfigService { get; }
		IDataSystemService DataSystem { get; }

		public StartupService(ISystemConfigService<AlphaSystemConfiguration> configService, IDataSystemService dataSystem) {
			this.ConfigService = configService;
			this.DataSystem = dataSystem;
			this.CurrentConfig = ConfigService.Config;
			//with
			//{
			//var si = new StartupInfo {
			//	AdminCredentials = new() {
			//		Login = "pi",
			//		Password = "Digital!2021"
			//	},
			//	DatabaseInfo = new() {
			//		HostName = "poseidonalpha.local",
			//		InstanceDbName = "Leviathan0x00",
			//		DbServerCredentials = new() {
			//			Login = "pi",
			//			Password = "Digital!2021"
			//		}
			//	}
			//};
			//};
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
			//this.DataSystem.Initialize();
			//save config
			//locate / creare database

		}
	}
}