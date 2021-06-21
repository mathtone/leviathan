//using Leviathan.Alpha.Configuration;
using Npgsql;
using System;

namespace Leviathan.Alpha.Database {
	public class InstanceConnectService : ConnectionService<NpgsqlConnection>, IInstanceConnectionService<NpgsqlConnection> {
		public InstanceConnectService(Func<NpgsqlConnection> connectionFactory) : base(connectionFactory) {
		}
	}
}