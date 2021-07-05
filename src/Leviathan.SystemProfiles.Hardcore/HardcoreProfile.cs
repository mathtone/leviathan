using Leviathan.Components;
using System;
using System.Threading.Tasks;

[assembly: LeviathanModule("Hardcore Mode Profile")]
namespace Leviathan.SystemProfiles.Hardcore {
	[SystemProfile("Hardcore Mode", "Do nothing, I will awaken my own Leviathan.  Leave us alone.")]
	public class HardcoreProfile : SystemProfileComponent {
		public HardcoreProfile(IComponentsService components) : base(components) {
		}

		public override Task Apply() {
			throw new NotImplementedException();
		}
	}
}
