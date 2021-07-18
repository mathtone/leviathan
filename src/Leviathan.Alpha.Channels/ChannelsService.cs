using Leviathan.Alpha.Logging;
using Leviathan.Services.SDK;
using System;

namespace Leviathan.Alpha.Channels {
	public interface IChannelsService : ILeviathanService {
	}

	[SingletonService(typeof(IChannelsService))]
	public class ChannelsService : LeviathanService, IChannelsService {

		ILoggingService _log;

		public ChannelsService(ILoggingService log) {
			this._log = log;
		}
	}
}