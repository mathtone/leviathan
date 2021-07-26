using Leviathan.SystemConfiguration.SDK;
using Leviathan.SystemProfiles.PostgreSQL;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.Basic {
	[SystemProfile, RequireProfile(typeof(PostgreSQLProfile))]
	public class BasicProfile : SystemProfileComponent {
		ILogger<BasicProfile> _log;

		[ProfileProperty("Instance Name","The name of this instance of THE LEVIATHAN.","TheLeviathan")]
		public string InstanceName { get; set; }

		public BasicProfile(ILogger<BasicProfile> log) {
			_log = log;
		}

		public override async Task Apply() {
			;
		}
	}
}