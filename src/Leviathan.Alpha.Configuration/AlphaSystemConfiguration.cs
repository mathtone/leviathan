using Leviathan.System;
using System;

namespace Leviathan.Alpha.Configuration {

	public record AlphaSystemConfiguration : SystemConfiguration {
		public StartupInfo StartupInfo { get; init; }
	}

	public record BasicLogin {
		public string Login { get; init; }
		public string Password { get; init; }
	}

	public record DatabaseInfo {
		public string HostName { get; init; }
		public string InstanceDbName { get; init; }
		public BasicLogin DbServerCredentials { get; init; }
	}

	public record StartupInfo {
		public BasicLogin AdminCredentials { get; set; }
		public DatabaseInfo DatabaseInfo { get; set; }
	}
}
