using System;

namespace Leviathan.SDK {
	public interface ILeviathanSystem : IAsyncInitialize {
		ILeviathanHostEnvironment HostEnvironment { get; }
		void ConfigureHost(ILeviathanHostEnvironment environment);
		event EventHandler SystemOnline;
		event EventHandler HostConfigured;
		event EventHandler HostStarted;
		event EventHandler HostStopping;
		event EventHandler HostStopped;
	}
}
