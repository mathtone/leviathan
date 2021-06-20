using Leviathan.Alpha.Database;
using Leviathan.Components;
using Leviathan.SDK;
using Leviathan.SystemProfiles.Basic;
using Npgsql;
using System.Reflection;
using System.Threading.Tasks;

[assembly: LeviathanPlugin]
namespace Leviathan.SystemProfiles.RoboTank {


	[SystemProfile("Robo-Tank", "Applies the 'Basic' profile & configures the Leviathan for the 'Robo-Tank' controller")]
	[RequireProfile(typeof(BasicProfile))]
	public class RoboTankProfile : SystemProfileComponent {

		public RoboTankProfile(IComponentsService components) :
			base(components) {
		}

		public override async Task Apply() {
			await base.ApplyRequired();
		}
	}
}