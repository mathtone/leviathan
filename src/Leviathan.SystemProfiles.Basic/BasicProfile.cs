using Leviathan.Alpha.Logging;
using Leviathan.SystemConfiguration.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.Basic {
	[SystemProfile]
	public class BasicProfile : SystemProfileComponent {

		[ProfileProperty("Instance Name","The name of this instance of THE LEVIATHAN.","TheLeviathan")]
		public string InstanceName { get; set; }

		public BasicProfile(ILoggingService log) {
			;
		}

		public override async Task Apply() {
			;
		}
	}
}