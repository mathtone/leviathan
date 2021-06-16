using Leviathan.Alpha.Npgsql;
using Leviathan.Services;
using Leviathan.System;
using System.Threading.Tasks;

namespace Leviathan.Alpha {
	public interface ISystemService : IAsyncInitialize {
		Task<SystemStatus> GetStatus();
	}

	public class SystemService : LeviathanServiceBase, ISystemService {

		protected ILeviathanDBService LeviathanDB { get; }
		protected ISystemConfigService ConfigProvider { get; }
		protected SystemConfiguration Config => ConfigProvider.Config;

		public SystemService(ISystemConfigService config, ILeviathanDBService leviathanDB) {
			this.ConfigProvider = config;
			this.LeviathanDB = leviathanDB;
		}

		protected async Task InitializeAsync() {
			//await LeviathanDB.Initialization;
		}

		public async Task<SystemStatus> GetStatus() =>
			new SystemStatus {
				InstanceName = Config.InstanceName,
				Database = await LeviathanDB.StatusReport()
			};
	}

	public class SystemStatus {
		public string InstanceName { get; set; }
		public DatabaseStatus Database { get; set; }
	}


}