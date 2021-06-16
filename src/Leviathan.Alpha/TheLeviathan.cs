using Leviathan.Alpha.Npgsql;
using Leviathan.Alpha.Secrets;
using Leviathan.Core;
using Leviathan.Services;
using Leviathan.System;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha {

	public class TheLeviathan : LeviathanServiceBase, ILeviathan {

		//public Task Initialization { get; }
		protected ISystemConfigService Config { get; }
		protected ILogger Log { get; }
		protected ISecrets Secrets { get; }
		protected ILeviathanDBService DataSystem { get; }

		public TheLeviathan(ISystemConfigService config, ILogger<TheLeviathan> logger) : base() {
			this.Config = config;
			this.Log = logger;
			Initialization = InitializeAsync();
		}

		private async Task InitializeAsync() => await Task.CompletedTask;

		public void Start() {
			//throw new NotImplementedException();
		}

		public void Stop() {
			//throw new NotImplementedException();
		}

		//protected override Task InitializeAsync() => Task.CompletedTask;
	}
}