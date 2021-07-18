using Leviathan.Alpha.Logging;
using Leviathan.SystemConfiguration.SDK;
using Leviathan.SystemProfiles.PostgreSQL;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.Basic {
	[SystemProfile, RequireProfile(typeof(PostgreSQLProfile))]
	public class BasicProfile : SystemProfileComponent {

		public BasicProfile(ILoggingService log) {
			;
		}

		public override async Task Apply() {
			;
		}
	}
}