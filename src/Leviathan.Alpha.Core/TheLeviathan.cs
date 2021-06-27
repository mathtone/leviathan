using Leviathan.Alpha.Components;
using Leviathan.Alpha.Startup;
using Leviathan.SDK;
using Leviathan.Services;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Core {
	public interface IAmLeviathan : IAsyncInitialize {
		Task Start(ILeviathanHostEnvironment environment);
		Task Stop();
	}

	public class TheLeviathan : ServiceComponent, IAmLeviathan {

		protected ILeviathanSystem System { get; }
		protected ILeviathanHostEnvironment Environment { get; set; }
		protected IStartupService Startup { get; set; }
		//protected IComponentsService Components { get; set; }

		public TheLeviathan(ILeviathanSystem system, IStartupService startup) : base() {
			this.System = system;
			this.Startup = startup;
			this.System.SystemOnline += System_SystemOnline;
			this.Initialize = InitializeAsync();
		}

		protected override async Task InitializeAsync() {
			await base.InitializeAsync();
		}

		public async Task Start(ILeviathanHostEnvironment environment) {
			await Initialize;
			this.Environment = environment;
			this.System.ConfigureHost(environment);
		}

		public async Task Stop() {
			await Initialize;
			;
		}

		private void System_SystemOnline(object sender, EventArgs e) => Task.Run(async () => {
			await Task.Yield();
			await Startup.ActivateProfile("Leviathan.SystemProfiles.FactoryReset.FactoryResetProfile"); ;
			await Startup.ActivateProfile("Leviathan.SystemProfiles.RoboTank.RoboTankProfile");
		});
	}
}