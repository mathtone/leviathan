using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Leviathan.Services.Core {

	public class CoreConfig {
		public string HostName { get; set; }
		public string DbName { get; set; }
	}

	public class LeviathanCore {

		bool running;
		protected CoreConfig config;
		protected ILogger<LeviathanCore> Logger { get; }
		protected IDbInitService DbInit { get; }

		public LeviathanCore(CoreConfig config, IDbInitService dbInitService, ILogger<LeviathanCore> logger) {
			this.config = config;
			this.DbInit = dbInitService;
			this.Logger = logger;
		}

		public void Start() {
			if (running) {
				throw new Exception("Core is already running");
			}
			else {
				running = true;
				DbInit.VerifyDatabase(config.DbName);
				this.Logger.LogInformation($"Database {config.DbName} initialized");
			}
		}
	}
}