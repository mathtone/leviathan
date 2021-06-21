//using Leviathan.Alpha.Configuration;
using Npgsql;
using System;

namespace Leviathan.Alpha.Database {
	public class SystemConnectService : ConnectionService<NpgsqlConnection>, ISystemConnectionService<NpgsqlConnection> {
		public SystemConnectService(Func<NpgsqlConnection> connectionFactory) : base(connectionFactory) {
		}
	}
}