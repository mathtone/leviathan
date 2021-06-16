using Leviathan.DataAccess;
using Leviathan.Services;
using Leviathan.System;
using Npgsql;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Npgsql {

	public class DatabaseStatus {
		public bool HostLocated { get; set; }
		public bool SystemDBLocated { get; set; }
		public bool InstanceDBLocated { get; set; }
		public string CurrentDbName { get; set; }
	}

	public interface ILeviathanDBService : IAsyncInitialize {
		Task<DatabaseStatus> StatusReport();
	}

	public interface ISystemDbConnectionProvider : IDbConnectionProvider<NpgsqlConnection, string> { }
	public interface IInstanceDbConnectionProvider : IDbConnectionProvider<NpgsqlConnection> { }

	public class LeviathanDBService : LeviathanServiceBase, ILeviathanDBService {

		ISystemConfigProvider ConfigProvider { get; }
		ISystemDbConnectionProvider SystemProvider { get; }
		IInstanceDbConnectionProvider InstanceProvider { get; }
		SystemConfiguration SysConfig => ConfigProvider.Config;

		public LeviathanDBService(ISystemConfigProvider sysConfig, ISystemDbConnectionProvider systemProvider, IInstanceDbConnectionProvider instanceProvider) {
			this.ConfigProvider = sysConfig;
			this.SystemProvider = systemProvider;
			this.InstanceProvider = instanceProvider;
			this.Initialization = InitializeAsync();
		}

		private async Task InitializeAsync() {
			var cfg = new {
				DbLogin = "pi",
				InstanceName = "Leviathan0x00",
				HostName = "poseidonalpha.local",
				DbPassword = "Digital!2021"
			};

			await ConfigProvider.Initialization;
			var sys = SystemProvider.Connect($"Host={cfg.HostName};Username={cfg.DbLogin};Database=postgres;Password={cfg.DbPassword};");
			sys.Open();
			sys.Close();
		}

		public async Task<DatabaseStatus> StatusReport() => new DatabaseStatus {
			CurrentDbName = SysConfig.InstanceName,
			//HostLocated = await LocateHost()
		};
	}
}