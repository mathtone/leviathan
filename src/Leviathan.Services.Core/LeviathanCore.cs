using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Leviathan.Services.Core {

	public class CoreConfig {
		public string HostName { get; set; }
		public string DbName { get; set; }
	}

	public interface ILeviathanCore {
		CoreStatus Status { get; }
	}

	public enum CoreStatus {
		Created,
		Launching,
		Initializing,
		AwatingConfig,
		Running,
		Closing
	}

	public enum DbStatus {
		Missing,
		Offline,
		Online
	}

	public class LeviathanCore : ILeviathanCore {

		bool running;
		protected CoreConfig config;
		protected ILogger<LeviathanCore> Logger { get; }
		
		protected IDbInitService DbInit { get; }
		
		public CoreStatus Status { get; private set; }

		public LeviathanCore(CoreConfig config, IDbInitService dbInitService, ILogger<LeviathanCore> logger) {
			this.config = config;
			this.DbInit = dbInitService;
			this.Logger = logger;
			this.Status = CoreStatus.Created;
		}

		public void Start() {
			if (running) {
				throw new Exception("Core is already running");
			}
			else {
				running = true;
				this.Status = CoreStatus.Launching;
				DbInit.VerifyDatabase(config.DbName);
				this.Logger.LogInformation($"Database {config.DbName} initialized");
				this.Status = CoreStatus.Running;
			}
		}

		public void Stop() {
			;
		}
	}
}