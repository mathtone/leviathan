using Leviathan.SystemConfiguration.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.SystemProfiles.FactoryReset {
	[SystemProfile]
	public class FactoryResetProfile : SystemProfileComponent {

		public async override Task Apply() {
			await Task.CompletedTask;
			;
		}
	}
}