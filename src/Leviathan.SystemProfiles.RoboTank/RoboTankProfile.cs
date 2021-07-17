using Leviathan.Alpha.Logging;
using Leviathan.SystemConfiguration.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.RoboTank {

	[SystemProfile]
	public class RoboTankProfile : SystemProfileComponent {

		ILoggingService _log;

		public RoboTankProfile(ILoggingService log) {
			this._log = log;
		}

		public override async Task Apply() {
			//await _log

		}
	}
}