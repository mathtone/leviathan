using Microsoft.Extensions.Logging;

namespace Leviathan.Services.Sdk {
	public abstract class ServiceBase {
		
		protected ILogger Log { get; }

		public ServiceBase(ILogger log) => Log = log;
	}
}