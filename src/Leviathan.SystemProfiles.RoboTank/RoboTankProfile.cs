using Leviathan.Alpha.Logging;
using Leviathan.SystemConfiguration.SDK;
using Leviathan.SystemProfiles.Basic;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.RoboTank {

	[SystemProfile, RequireProfile(typeof(BasicProfile))]
	public class RoboTankProfile : SystemProfileComponent {

		ILoggingService _log;

		public RoboTankProfile(ILoggingService log) {
			this._log = log;
		}

		public override async Task Apply() {
			await Task.CompletedTask;
		}
	}
}