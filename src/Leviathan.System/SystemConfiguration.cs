using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan.System {

	public class SystemConfiguration {
		public string InstanceName { get; set; }
		//public string DbName { get; aet; }
		public bool Activated { get; set; }
		//public Dictionary<>
	}

	public interface ISystemConfigProvider {
		SystemConfiguration Config { get; }
		Task Initialization { get; }
	}

	public interface ISystemConfigService : ISystemConfigProvider {
		Task SaveAsync(SystemConfiguration config);
		Task<SystemConfiguration> ReloadAsync();
	}

	public class ConfigurationEventArgs : EventArgs {

	}
}