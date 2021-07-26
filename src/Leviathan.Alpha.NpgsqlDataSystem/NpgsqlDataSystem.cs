using Leviathan.Services.SDK;
using Microsoft.Extensions.Logging;
using System;

namespace Leviathan.Alpha.NpgsqlDataSystem {
	public interface IDbDataSystem {

	}
	public interface INpgsqlDataSystem : IDbDataSystem {

	}

	[SingletonService(typeof(INpgsqlDataSystem), typeof(IDbDataSystem))]
	public class NpgsqlDataSystem : INpgsqlDataSystem {

		ILogger<NpgsqlDataSystem> _log;

		public NpgsqlDataSystem(ILogger<NpgsqlDataSystem> log) {
			this._log = log;
		}
	}
}