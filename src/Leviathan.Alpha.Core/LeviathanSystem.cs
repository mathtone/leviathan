using Leviathan.SDK;
using Leviathan.Services;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Core {
	

	public class LeviathanSystem : ServiceComponent, ILeviathanSystem {
		public LeviathanSystem() { }

		public ILeviathanHostEnvironment HostEnvironment { get; private set; }

		public event EventHandler HostConfigured;
		public event EventHandler HostStarted;
		public event EventHandler HostStopping;
		public event EventHandler HostStopped;
		public event EventHandler SystemOnline;

		public void ConfigureHost(ILeviathanHostEnvironment environment) {
			if (this.HostEnvironment != null) {
				throw new Exception("Environment already configured");
			}
			this.HostEnvironment = environment;
			environment.Lifetime.ApplicationStarted.Register(OnStart);
			environment.Lifetime.ApplicationStopping.Register(OnStopping);
			environment.Lifetime.ApplicationStopped.Register(OnStop);
			HostConfigured.Raise(this, new EventArgs());
		}

		void OnStart() {
			HostStarted.Raise(this, new EventArgs());
			SystemOnline.Raise(this, new EventArgs());
		}
		void OnStopping() => HostStopping.Raise(this, new EventArgs());
		void OnStop() => HostStopped.Raise(this, new EventArgs());
	}
}