using Leviathan.Alpha.Logging;
using Leviathan.Services.SDK;
using System;

namespace Leviathan.Alpha.NpgsqlDataSystem {
	public interface IDbDataSystem {

	}
	public interface INpgsqlDataSystem : IDbDataSystem {

	}

	[SingletonService(typeof(INpgsqlDataSystem), typeof(IDbDataSystem))]
	public class NpgsqlDataSystem : INpgsqlDataSystem {

		ILoggingService _log;

		public NpgsqlDataSystem(ILoggingService log) {
			this._log = log;
		}
	}
}