using Leviathan.SystemConfiguration.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.Hardcore {

	[SystemProfile]
	public class HardcoreProfile : SystemProfileComponent {
		ILogger<HardcoreProfile> _log;
		public HardcoreProfile(ILogger<HardcoreProfile> log) {
			_log = log;
		}

		public override async Task Apply() {
			;
		}
	}
}
