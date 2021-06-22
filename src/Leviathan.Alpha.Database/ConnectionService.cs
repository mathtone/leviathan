//using Leviathan.Alpha.Configuration;
using System;
using System.Data;

namespace Leviathan.Alpha.Database {
	public interface IDbConnectionService<out CN> where CN : IDbConnection {
		CN Connect();
	}

	public class ConnectionService<CN> : IDbConnectionService<CN> where CN : IDbConnection {
		readonly Func<CN> connectionFactory;

		public CN Connect() => connectionFactory();
		
		public ConnectionService(Func<CN> connectionFactory) {
			this.connectionFactory = connectionFactory;
		}
	}
}