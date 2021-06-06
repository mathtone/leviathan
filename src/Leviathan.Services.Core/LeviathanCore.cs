using System;
using System.Threading.Tasks;
using Leviathan.DataAccess;
using Leviathan.Hardware;
using Leviathan.Services.Core.Hardware;
using Microsoft.Extensions.Logging;

namespace Leviathan.Services.Core {

	public class CoreConfig {
		public string HostName { get; set; }
		public string DbName { get; set; }
	}

	public interface ILeviathanCore {
		CoreStatus Status { get; }
		void Start();
		void Stop();
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

		protected bool running;
		protected CoreConfig config;
		protected ILogger<LeviathanCore> Logger { get; }
		protected IDbInitService DbInit { get; }
		protected IHardwareService Hardware { get; }

		public CoreStatus Status { get; private set; }

		public LeviathanCore(CoreConfig config, IDbInitService dbInitService, IHardwareService hardware, ILogger<LeviathanCore> logger) {
			this.config = config;
			this.DbInit = dbInitService;
			this.Hardware = hardware;
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