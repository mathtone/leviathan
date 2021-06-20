using Leviathan.Components;
using Leviathan.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Components.Profiles {
	public interface ISystemProfileComponent {
		Task Apply();
	}

	public abstract class SystemProfileComponent : ISystemProfileComponent {
		public abstract Task Apply();
	}

	[SystemProfile("Robo-Tank", "Configure the Leviathan for the 'Robo-Tank' controller")]
	public class RoboTankProfile : SystemProfileComponent {

		ILeviathanSystem System { get; }

		public RoboTankProfile(ILeviathanSystem system) {
			this.System = system;
		}
		public override async Task Apply() {
			await Task.CompletedTask;

			;
		}
	}

	[SystemProfile("Basic", "Configures support data for a basic installation, does not configure drivers or connectors.")]
	public class BasicProfile : SystemProfileComponent {
		public override async Task Apply() {
			await Task.CompletedTask;
		}
	}

	[SystemProfile("Hardcore", "Do nothing.  I'm gonna do it.")]
	public class HardcoreMode : SystemProfileComponent {
		public override async Task Apply() {
			await Task.CompletedTask;
		}
	}
}