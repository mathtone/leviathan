using Leviathan.Services.SDK;
using System;

namespace Leviathan.Alpha.Logging {
	public interface ILoggingService {
		void Write(string message);
	}

	[SingletonService(typeof(ILoggingService))]
	public class LoggingService : ILoggingService {
		public void Write(string message) {
			;
		}
	}
}
