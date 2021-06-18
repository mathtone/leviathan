using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan.System {

	public record SystemConfiguration {
		public string InstanceName { get; set; }
		//public string DbName { get; aet; }
		public bool Activated { get; set; }
		//public StartupInfo StartupOptions { get; set; }
		//public Dictionary<>
	}

	//public class StartupOptions {

	//	public string DBServerHost { get; set; }
	//	public string DBServerLogin { get; set; }
	//	public string DBServerPwd { get; set; }

	//	public bool StartupRequired { get; set; }

	//}

	public interface ISystemConfigProvider<out CFG> {
		CFG Config { get; }
		Task Initialize { get; }
	}

	public interface ISystemConfigService<CFG> : ISystemConfigProvider<CFG> {

		Task SaveAsync(CFG config);
		Task<CFG> ReloadAsync();
	}

	public class ConfigurationEventArgs : EventArgs {
	}
}